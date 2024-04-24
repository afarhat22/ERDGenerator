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
using Windows.UI.ViewManagement.Core;

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
            int entityX = 500;
            int entityY = 50;

            int relationshipX = 800;
            int relationshipY = 50;

            foreach (Entity entity in entities)
            {
                AddEntity(entityX, entityY, entity.name);

                //Sets entity coordinate
                entity.X = entityX;
                entity.Y = entityY;

                //Updates next entities location
                entityY += 150;
            }

            foreach (Relationship relationship in relationships)
            {
                //int entity1 = entities.FindIndex(relationship.entity1);

            }
        }
        private void AddEntity(double x, double y, string name)
        {
            Rectangle rect = new Rectangle
            {
                Width = 150,
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
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center
            };
            Canvas.SetLeft(text, x + 10);
            Canvas.SetTop(text, y + 15);
            DrawingCanvas.Children.Add(text);
        }

        private void AddRelationship(String name, int x, int y, 
            int line1x1, int line1y1, 
            int line1x2, int line1y2, 
            int line2x1, int line2y1, 
            int line2x2, int line2y2)
        {
            Polygon polygon = new Polygon
            {
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.LightGray),

            };

            PointCollection points = new PointCollection
            {
                new Point(x, y),    //top
                new Point(x + 50, y + 75), //right
                new Point(x, y + 150),   //bottom
                new Point(x - 50, y + 75)   //left
            };

            polygon.Points = points;
            DrawingCanvas.Children.Add(polygon);
            TextBlock text = new TextBlock
            {
                Text = name,
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center
            };
            Canvas.SetLeft(text, x - 45);
            Canvas.SetTop(text, y + 15);
            DrawingCanvas.Children.Add(text);


        }
    }
}
