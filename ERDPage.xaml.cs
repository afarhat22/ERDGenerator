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
using Windows.UI.Xaml.Shapes;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ERDGenerator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ERDPage : Page
    {

        List<Entity> entities = Application.Current.Resources["Entities"] as List<Entity>;
        List<Attribute> attributes = Application.Current.Resources["Attributes"] as List<Attribute>;
        List<Relationship> relationships = Application.Current.Resources["Relationships"] as List<Relationship>;
        public ERDPage()
        {
            this.InitializeComponent();
            
            DrawElements();
        }

        public void DrawElements()
        {
            foreach (Entity entity in entities)
            {

            }
        }
        private void AddEntity(double x, double y, string name)
        {
            Rectangle rect = new Rectangle
            {
                Width = 100,
                Height = 50,
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.LightGray)
            };

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            DrawingCanvas.Children.Add(rect);

            TextBlock text = new TextBlock
            {
                Text = name,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center
            };
            Canvas.SetLeft(text, x + 10);
            Canvas.SetTop(text, y + 15);
            DrawingCanvas.Children.Add(text);
        }
    }
}
