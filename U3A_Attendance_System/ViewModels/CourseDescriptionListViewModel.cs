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
                        _facade.FetchCourseDescriptions().OrderBy(cd => cd.CourseNumber)
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
                tabbedView.View2.FetchCDInstances((ICourseDescription)obj);
            tabbedView.Refresh();
            tabbedView.SelectedTab = 2;
        }

        public void DeleteDescriptionConfirm(ICourseDescription cd)
        {
            //if (MessageBox.Show("Are you sure you want to delete the Course Descripton: '" + cd.Title + "' ?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    _facade.DeleteCourseDescription(cd.Id);

            settings.Title = "Delete Coordinator";
            _wm.ShowDialog(new DeleteViewModel(cd), null, settings);
            NotifyOfPropertyChange("CourseDescriptions");
        }

        public void SearchTitles()
        {
            if (TitleSearch != null)
            {
                string search = TitleSearch.ToUpper();
                //CourseDescriptions = _facade.FetchCourseDescriptions().Where(cd => cd.Title.Contains(search));
                this.Refresh();
            }
        }

        #endregion

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
