using System.ComponentModel;
using System.Windows;
using HealthInstitution.Core.Features.NotificationManagement.Service;
using HealthInstitution.Core.Ninject;

namespace HealthInstitution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void WindowClosing(object sender, CancelEventArgs e)
        {
            IPrescribedMedicineNotificationService prescribedMedicineNotificationService = ServiceLocator.Get<IPrescribedMedicineNotificationService>();
            prescribedMedicineNotificationService.DeleteAllUpcomingMedicinesNotifications();
        }
    }
}
