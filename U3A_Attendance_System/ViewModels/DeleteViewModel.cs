﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class DeleteViewModel : BaseViewModel
    {
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        object itemToDelete;
        private object viewModel;

        public DeleteViewModel(ICourseDescription courseDescription)
        {
            Label1 = courseDescription.Title;
            itemToDelete = courseDescription;
        }

        public DeleteViewModel(IVenue venue)
        {
            Label1 = venue.Name;
            Label2 = venue.Address;
            itemToDelete = venue;
        }

        public DeleteViewModel(ICoordinator coordinator)
        {
            Label1 = coordinator.Name;
            Label2 = coordinator.Email;
            itemToDelete = coordinator;
        }

        public DeleteViewModel(ICourseInstance instance, CourseInstanceListViewModel model)
        {
            Label1 = instance.CourseDescription.Title;
            Label2 = instance.CourseCode;
            itemToDelete = instance;
            viewModel = model;
        }

        public DeleteViewModel(ISession session)
        {
            Label1 = session.CourseInstance.CourseDescription.Title;
            Label2 = session.Date.ToString();
            itemToDelete = session;
        }

        public void Delete()
        {
            if (itemToDelete != null)
            {
                if (itemToDelete is ICourseDescription)
                {
                    try
                    {
                        _facade.DeleteCourseDescription((itemToDelete as ICourseDescription).Id);
                    }
                    catch (AssociationDependencyException e)
                    { 
                    }
                }

                if (itemToDelete is IVenue)
                {
                    IVenue venue = (IVenue)itemToDelete;

                    try
                    {
                        _facade.DeleteVenue(venue.Id, venue.RegionId, venue.SuburbId);
                    }
                    catch (AssociationDependencyException e)
                    {
                        settings.Title = "Delete Venue Locations";
                        _wm.ShowDialog(new WarningViewModel((IVenue)venue), null, settings);
                    }
                }

                if (itemToDelete is ICoordinator)
                {
                    ICoordinator coordinator = (ICoordinator)itemToDelete;
                    try
                    {
                        _facade.DeleteCoordinator(coordinator.Id);
                    }
                    catch (InvalidOperationException e)
                    {
                      
                    }
                }

                if (itemToDelete is ICourseInstance)
                {
                    ICourseInstance courseInstance = (ICourseInstance)itemToDelete;
                    var model = viewModel as CourseInstanceListViewModel;

                    try
                    {
                        _facade.DeleteCourseInstance(courseInstance.Id, courseInstance.RegionId);
                        model.NotifyOfPropertyChange("InstancesList");
                    }
                    catch (AssociationDependencyException e)
                    {
                        settings.Title = "Delete Instance Sessions";
                        _wm.ShowDialog(new WarningViewModel((ICourseInstance)courseInstance), null, settings);
                    }


                }

                if (itemToDelete is ISession)
                {
                    ISession session = (ISession)itemToDelete;

                    try
                    {
                        _facade.DeleteSession(session.Id, session.CourseInstanceId, session.CourseInstance.RegionId);
                    }

                    catch (AssociationDependencyException e)
                    {
                        settings.Title = "Delete Session Attendances";
                        _wm.ShowDialog(new WarningViewModel((ISession)session), null, settings);
                        NotifyOfPropertyChange("Sessions");
                    }
                }
            }
        }
    }
}

