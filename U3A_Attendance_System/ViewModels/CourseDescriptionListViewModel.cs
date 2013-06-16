using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using U3A_Attendance_Model;
using U3A_Attendance_System.Views;

namespace U3A_Attendance_System.ViewModels
{
    public class CourseDescriptionListViewModel : BaseViewModel
    {
        private ObservableCollection<ICourseDescription> courseDescriptions;
        private string titleSearch;
        private MainViewModel main;
        private TabbedViewModel tabbedView;

        #region Properties


        public ObservableCollection<ICourseDescription> CourseDescriptions
        {
            get
            {
                courseDescriptions = new ObservableCollection<ICourseDescription>
                    (
                        _facade.FetchCourseDescriptions().OrderBy(cd => cd.Title)
                    );
                return courseDescriptions;
            } 
        }

        public string TitleSearch { get { return titleSearch; } set { titleSearch = value; SearchTitles(); } }

        #endregion

        public CourseDescriptionListViewModel(MainViewModel main, TabbedViewModel tabbedView)
        {
            this.main = main;
            this.tabbedView = tabbedView;
        }

        #region Commands/Behaviours

        public void ShowCDEdit(Object cd)
        {
            if (cd is ICourseDescription)
            {
                settings.Title = "Edit course description";
                _wm.ShowDialog(new CourseDescriptionEditViewModel((ICourseDescription)cd), null, settings);
                NotifyOfPropertyChange(() => CourseDescriptions);
            }
            else
            {
                settings.Title = "Create course description";
                _wm.ShowDialog(new CourseDescriptionEditViewModel(), null, settings);
                NotifyOfPropertyChange("CourseDescriptions");
                this.Refresh();
            }
        }

        public void ShowCIList(Object obj)
        {
            if (obj is ICourseDescription)
                tabbedView.View2.FetchCDInstances((ICourseDescription)obj, this);
            tabbedView.SelectedTab = 2;
        }

        public void DeleteDescriptionConfirm(ICourseDescription cd)
        {
            settings.Title = "Delete Coordinator";
            _wm.ShowDialog(new DeleteViewModel(cd), null, settings);
            NotifyOfPropertyChange("CourseDescriptions");
        }

        public void SearchTitles()
        {
            if (TitleSearch != null)
            {
                string search = TitleSearch.ToUpper();
                this.Refresh();
            }
        }

        #endregion

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
