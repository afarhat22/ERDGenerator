using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static ERDGenerator.Relationship;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ERDGenerator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {



        List<Relationship> relationships;
        List<Entity> entities;
        List<Attribute> attributes;

        AllDiagramBoxes boxes;

        public MainPage()
        {
            this.InitializeComponent();

            relationships = new List<Relationship>();
            entities = new List<Entity>();
            attributes = new List<Attribute>();

             
        }

        private void saveEntity_Click(object sender, RoutedEventArgs e)
        {
            if (entityName.Text != String.Empty)
            {
                entities.Add(new Entity(entityName.Text));
                attribute_entitySelector.Items.Add(entityName.Text);
                relationship_entity1.Items.Add(entityName.Text);
                relationship_entity2.Items.Add(entityName.Text);
                entityName.Text = String.Empty;
            } else
            {
                _ =ShowDialog();
            }
            
        }

        private void saveAttribute_Click(object sender, RoutedEventArgs e)
        {
            if (attributeName.Text != String.Empty && attribute_entitySelector.SelectedIndex != -1)
            {
                attributes.Add(new Attribute(attributeName.Text));
                entities.ElementAt(attribute_entitySelector.SelectedIndex).AddAttribute(attributes.Last());
                attributeName.Text = String.Empty;
                attribute_entitySelector.SelectedIndex = -1;
            }
            else
            {
                _ = ShowDialog();
            }

        }

        private void saveAttribute1_Click(object sender, RoutedEventArgs e)
        {
            if (relationshipName.Text != String.Empty && relationship_entity1.SelectedIndex != -1 &&
                relationship_entity1type.SelectedIndex != -1 && relationship_entity2.SelectedIndex != -1 && relationship_entity2Type.SelectedIndex != -1)
            {
                relationships.Add(new Relationship(relationshipName.Text, entities.ElementAt(relationship_entity1.SelectedIndex), entities.ElementAt(relationship_entity2.SelectedIndex), relationship_entity1type.SelectedIndex, relationship_entity2Type.SelectedIndex));
                relationship_entity1type.SelectedIndex = -1;
                relationship_entity2Type.SelectedIndex = -1;
                relationship_entity1.SelectedIndex = -1;
                relationship_entity2.SelectedIndex = -1;
                relationshipName.Text = String.Empty;

            }
            else
            {
                _ = ShowDialog();
            }
        }

        
        private void generateErd_Click(object sender, RoutedEventArgs e)
        {
            boxes = new AllDiagramBoxes
            {
                entities = entities,
                relationships = relationships,
                attributes = attributes
            };

            
            this.Frame.Navigate(typeof(ERDPage), boxes);
        }

        private async Task ShowDialog()
        {
            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Invalid Input",
                Content = "One of the inputs isn't filled in, please check all input fields",
                CloseButtonText = "OK",
            };

            await contentDialog.ShowAsync();
        }
    }
}
