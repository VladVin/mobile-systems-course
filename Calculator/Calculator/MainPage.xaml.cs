using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public enum Operations { None, Plus, Minus, Mult, Devision }
        private Operations choosedOperation;
        private CalcEngine calcEngine;
        private double a, b, res;
        private double savedVal;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            calcEngine = new CalcEngine();
            initialize();
        }

        public void initialize()
        {
            outbox.Text = "";
            bnPlus.IsEnabled = true;
            bnMinus.IsEnabled = true;
            bnMult.IsEnabled = true;
            bnDev.IsEnabled = true;
            bnEq.IsEnabled = false;
            choosedOperation = Operations.None;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            outbox.Text += ((Button)sender).Content;
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            if (outbox.Text == "") return;

            a = Convert.ToDouble(outbox.Text);
            outbox.Text = "";
            bnEq.IsEnabled = true;

            switch(((Button)sender).Name)
            {
                case "bnPlus":
                    choosedOperation = Operations.Plus;
                    break;
                case "bnMinus":
                    choosedOperation = Operations.Minus;
                    break;
                case "bnMult":
                    choosedOperation = Operations.Mult;
                    break;
                case "bnDev":
                    choosedOperation = Operations.Devision;
                    break;
            }

            bnPlus.IsEnabled = false;
            bnMinus.IsEnabled = false;
            bnMult.IsEnabled = false;
            bnDev.IsEnabled = false;
        }

        private void Point_Click(object sender, RoutedEventArgs e)
        {
            if (!(outbox.Text.Split('.').Length >= 2))
            {
                outbox.Text += ".";
            }
        }

        private void Eq_Click(object sender, RoutedEventArgs e)
        {
            if (outbox.Text == "" || choosedOperation == Operations.None) return;

            b = Convert.ToDouble(outbox.Text);

            res = calcEngine.calculate(a, b, choosedOperation);

            a = 0.0;
            b = 0.0;

            outbox.Text = Convert.ToString(res);
            bnPlus.IsEnabled = true;
            bnMinus.IsEnabled = true;
            bnMult.IsEnabled = true;
            bnDev.IsEnabled = true;
        }

        private void Memory_Click(object sender, RoutedEventArgs e)
        {
            savedVal = Convert.ToDouble(outbox.Text);
        }

        private void Return_Memory_Click(object sender, RoutedEventArgs e)
        {
            if (outbox.Text != "")
            {
                outbox.Text = Convert.ToString(savedVal);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (outbox.Text != "")
            {
                string value = outbox.Text;
                outbox.Text = value.Substring(0, value.Length - 1);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            initialize();
        }
    }
}
