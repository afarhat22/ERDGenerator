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
using System.ComponentModel.DataAnnotations;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ERDGenerator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ERDPage : Page
    {
        

        List<Entity > entities;
        List<Relationship> relationships;
        List<Attribute> attributes;

        
        public ERDPage()
        {
            this.InitializeComponent();
            this.entities = new List<Entity>();
            this.relationships = new List<Relationship>();
            this.attributes = new List<Attribute>();
            
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AllDiagramBoxes boxes = e.Parameter as AllDiagramBoxes;
            if (boxes != null && boxes.attributes != null && boxes.relationships != null && boxes.entities != null)
            {
                foreach (Entity entities in boxes.entities)
                {
                    this.entities.Add(entities);
                }

                foreach (Relationship relationship in boxes.relationships)
                {
                    this.relationships.Add(relationship);
                }

                foreach(Attribute attribute in boxes.attributes)
                {
                    this.attributes.Add(attribute);
                }
            }
            else
            {
                throw new InvalidDataException("boxes is null");
            }

            DrawElements();

        }
        public void DrawElements()
        {
            int entityX = 500;
            int entityY = 75;

            int relationshipX = 1000;
            int relationshipY = 100;

            int attributeX = 200;
            int attributeY = 25;

            foreach (Entity entity in entities)
            {
                AddEntity(entityX, entityY, entity.name);

                //Sets entity coordinate
                entity.X = entityX;
                entity.Y = entityY;

                //Updates next entities location
                entityY += 150;

                foreach (Attribute attribute in entity.GetAttributes())
                { 
                    AddAttribute(attributeX, attributeY, attribute.name, entity);
                    attributeY += 150;
                }
            }

            foreach (Relationship relationship in relationships)
            {
                int entity1XCoordinate = relationship.entity1.X + 150;
                int entity1YCoordinate = relationship.entity1.Y + 25;

                int entity2XCoordinate = relationship.entity2.X + 150;
                int entity2YCoordinate = relationship.entity2.Y + 25;

                AddRelationship(relationship.name, relationshipX, relationshipY,
                    entity1XCoordinate, entity1YCoordinate, entity2XCoordinate, entity2YCoordinate,
                    relationship.e1Type, relationship.e2Type);

                relationshipY += 150;

            }

            
        }

        private void AddAttribute(int x, int y,  string name, Entity entity)
        {
            Line line = new Line
            {
                X1 = x + 75,
                Y1 = y + 25,
                X2 = entity.X,
                Y2 = entity.Y + 25,
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 2,
            };
            DrawingCanvas.Children.Add(line);
            Ellipse circle = new Ellipse()
            {
                Width = 150,
                Height = 50,
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.LightGray)
            };

            DrawingCanvas.Children.Add(circle);
            Canvas.SetLeft(circle, x);
            Canvas.SetTop(circle, y);

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
            int line2x2, int line2y2,
            Relationship.relationshipType type1, Relationship.relationshipType type2)
        {
            //create rhombus
            Polygon polygon = new Polygon
            {
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.LightGray),

            };

            //points for rhombus
            PointCollection points = new PointCollection
            {
                new Point(x, y),    //top
                new Point(x + 75, y + 25), //right
                new Point(x, y + 50),   //bottom
                new Point(x - 75, y + 25)   //left
            };
            //draw it
            polygon.Points = points;
            DrawingCanvas.Children.Add(polygon);

            //create text for in rhombus
            TextBlock text = new TextBlock
            {
                Text = name,
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center
            };
            //align and draw it
            Canvas.SetLeft(text, x - 45);
            Canvas.SetTop(text, y + 15);
            DrawingCanvas.Children.Add(text);
            TextBlock typeText = new TextBlock
            {
                Text = $"{type1} to {type2}",
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center
            };
            Canvas.SetLeft(typeText, x + 150);
            Canvas.SetTop(typeText, y);
            DrawingCanvas.Children.Add(typeText);

            int line_startX = x - 75;
            int line_startY = y + 25;

            Line line1 = new Line
            {
                X1 = line_startX,
                Y1 = line_startY,
                X2 = line1x1,
                Y2 = line1y1,
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 2,
            };

            Line line2 = new Line
            {
                X1 = line_startX,
                Y1 = line_startY,
                X2 = line2x2,
                Y2 = line2y2,
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 2,
            };
            DrawingCanvas.Children.Add(line2);
            DrawingCanvas.Children.Add(line1);
            




        }
    }
}
