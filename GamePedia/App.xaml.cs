using GamePedia.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Search;
using GamePedia.Data;
using GamePedia.DataModel;
using Windows.UI.ApplicationSettings;
using Callisto.Controls;
using Windows.UI;



// The Grid App template is documented at http://go.microsoft.com/fwlink/?LinkId=234226

namespace GamePedia
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private Color _background = Color.FromArgb(255, 0, 77, 96);
        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Do not repeat app initialization when already running, just ensure that
            // the window is active
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
            {
                Window.Current.Activate();
                return;
            }

            SearchPane.GetForCurrentView().SuggestionsRequested += App_SuggestionsRequested;

            // Create a Frame to act as the navigation context and associate it with
            // a SuspensionManager key
            var rootFrame = new Frame();
            SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

            SettingsPane.GetForCurrentView().CommandsRequested += OnCommandRequested;

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                // Restore the saved session state only when appropriate
                await SuspensionManager.RestoreAsync();
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(GroupedItemsPage), "AllGroups"))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Place the frame in the current Window and ensure that it is active
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }
        

        private void App_SuggestionsRequested(SearchPane sender, SearchPaneSuggestionsRequestedEventArgs args)
        {
            string query = args.QueryText.ToLower();
            var allQuery = GamePediaDataSource.GetGroups("AllGroups");

            foreach (var group in allQuery.Distinct())
            {
                foreach (var item in group.Items.Distinct())
                {
                    if (item.Title.ToLower().StartsWith(query))
                        args.Request.SearchSuggestionCollection.AppendQuerySuggestion(item.Title);
                }
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        /// <summary>
        /// Invoked when the application is activated to display search results.
        /// </summary>
        /// <param name="args">Details about the activation request.</param>
        protected async override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.NotRunning ||
                args.PreviousExecutionState == ApplicationExecutionState.ClosedByUser ||
                args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            { 
                //Carrega item data

                //await GamePediaDataSource.LoadLocalDataAsync();

                //Registra handler para App_SuggestionsRequested

                SearchPane.GetForCurrentView().SuggestionsRequested += App_SuggestionsRequested;
                SettingsPane.GetForCurrentView().CommandsRequested += OnCommandRequested;
                //SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                //Window.Current.Content = rootFrame;
            }
            GamePedia.SearchPage.Activate(args.QueryText, args.PreviousExecutionState);
        }

        /// <summary>
        /// Invoked when the application is activated as the target of a sharing operation.
        /// </summary>
        /// <param name="args">Details about the activation request.</param>
        protected override void OnShareTargetActivated(Windows.ApplicationModel.Activation.ShareTargetActivatedEventArgs args)
        {
            var shareTargetPage = new GamePedia.SharePage();
            shareTargetPage.Activate(args);
        }

        private void OnCommandRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            // Add an About command
            var about = new SettingsCommand("about", "About", (handler) =>
            {
                var settings = new SettingsFlyout();
                settings.Content = new AboutPage();
                settings.HeaderBrush = new SolidColorBrush(_background);
                settings.Background = new SolidColorBrush(_background);
                settings.HeaderText = "About";
                settings.IsOpen = true;
            });

            args.Request.ApplicationCommands.Add(about);

        }
    }
}
