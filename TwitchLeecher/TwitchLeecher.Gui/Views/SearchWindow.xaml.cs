﻿using System;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Threading;
using TwitchLeecher.Core.Models;
using TwitchLeecher.Gui.Interfaces;

namespace TwitchLeecher.Gui.Views
{
    public partial class SearchWindow : Window
    {
        private IDialogService dialogService;

        public SearchWindow(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            InitializeComponent();

            WindowChrome windowChrome = new WindowChrome()
            {
                CaptionHeight = 51,
                CornerRadius = new CornerRadius(0),
                GlassFrameThickness = new Thickness(0),
                NonClientFrameEdges = NonClientFrameEdges.None,
                ResizeBorderThickness = new Thickness(0),
                UseAeroCaptionButtons = false
            };

            WindowChrome.SetWindowChrome(this, windowChrome);

            this.cmbLoadLimit.ItemsSource = Preferences.GetLoadLimits();

            this.Loaded += SearchRequestView_Loaded;
        }

        private void SearchRequestView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                this.txtUsername.Focus();

                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.DataBind, new Action(() =>
                {
                    this.txtUsername.SelectAll();
                }));
            }
            catch (Exception ex)
            {
                this.dialogService.ShowAndLogException(ex);
            }
        }
    }
}