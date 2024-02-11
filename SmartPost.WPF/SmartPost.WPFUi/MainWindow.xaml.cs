﻿using System.Windows;
using System.Windows.Input;

namespace SmartPost.WPFUi
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

        private void br_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void rbDashboard_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbSklad_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbDokon_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbTarix_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbSettings_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbLogOut_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}