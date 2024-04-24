using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
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



        List<Entity> entities = Application.Current.Resources["Entities"] as List<Entity>;
        List<Attribute> attributes = Application.Current.Resources["Attributes"] as List<Attribute>;
        List<Relationship> relationships = Application.Current.Resources["Relationships"] as List<Relationship>;


        public MainPage()
        {
            this.InitializeComponent();

            
        }

        private void saveEntity_Click(object sender, RoutedEventArgs e)
        {
               if (!string.IsNullOrWhiteSpace(entityName.Text))
               {
                    Entity newEntity = new Entity(entityName.Text);
                    entities.Add(newEntity);
                    entitiesPreview.Text += newEntity.name + Environment.NewLine; // Update your entities preview text block
                    attribute_entitySelector.Items.Add(newEntity.name);
                    relationship_entity1.Items.Add(newEntity.name);
                    relationship_entity2.Items.Add(newEntity.name);
                    entityName.Text = string.Empty; // Clear the input field
               }

          }

        private void saveAttribute_Click(object sender, RoutedEventArgs e)
        {
               if (!string.IsNullOrWhiteSpace(attributeName.Text) && attribute_entitySelector.SelectedIndex >= 0)
               {
                    Entity selectedEntity = entities.ElementAt(attribute_entitySelector.SelectedIndex);
                    Attribute newAttribute = new Attribute(attributeName.Text);
                    selectedEntity.AddAttribute(newAttribute);
                    attributes.Add(newAttribute);
                    attributesPreview.Text += selectedEntity.name + "." + newAttribute.name + Environment.NewLine; // Update your attributes preview text block
                    attributeName.Text = string.Empty; // Clear the input field
                    attribute_entitySelector.SelectedIndex = -1; // Reset the combo box
               }

          }

        private void saveRelationship_Click(object sender, RoutedEventArgs e)
        {
               if (!string.IsNullOrWhiteSpace(relationshipName.Text) &&
         relationship_entity1.SelectedIndex >= 0 &&
         relationship_entity2.SelectedIndex >= 0)
               {
                    Entity entity1 = entities.ElementAt(relationship_entity1.SelectedIndex);
                    Entity entity2 = entities.ElementAt(relationship_entity2.SelectedIndex);
                    Relationship newRelationship = new Relationship(
                        relationshipName.Text,
                        entity1,
                        entity2,
                        relationship_entity1type.SelectedIndex,
                        relationship_entity2Type.SelectedIndex
                    );
                    relationships.Add(newRelationship);
                    relationshipsPreview.Text += $"{entity1.name} {((relationshipType)relationship_entity1type.SelectedIndex).ToString()} - " +
                                                 $"{((relationshipType)relationship_entity2Type.SelectedIndex).ToString()} {entity2.name} " +
                                                 $"{newRelationship.name}{Environment.NewLine}"; // Update your relationships preview text block
                                                                                                 // Reset the UI elements
                    relationshipName.Text = string.Empty;
                    relationship_entity1type.SelectedIndex = -1;
                    relationship_entity2Type.SelectedIndex = -1;
                    relationship_entity1.SelectedIndex = -1;
                    relationship_entity2.SelectedIndex = -1;
               }
          }

          private void generateErd_Click(object sender, RoutedEventArgs e)
          {
               // Serialize your lists to JSON strings.
               var entitiesJson = JsonConvert.SerializeObject(entities);
               var attributesJson = JsonConvert.SerializeObject(attributes);
               var relationshipsJson = JsonConvert.SerializeObject(relationships);

               // Create an anonymous object with all the JSON strings
               var navigationData = new
               {
                    Entities = entitiesJson,
                    Attributes = attributesJson,
                    Relationships = relationshipsJson
               };

               // Serialize the entire anonymous object to a JSON string
               var navigationDataJson = JsonConvert.SerializeObject(navigationData);

               // Navigate to ERDPage and pass the serialized JSON string as the parameter
               Frame.Navigate(typeof(ERDPage), navigationDataJson);
          }


     }
}
