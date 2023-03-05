using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace EjednevnikWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            datepiker.SelectedDate = DateTime.Today;
        }

        private void select_date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                var desereolize = Json.desereolize<Dictionary<string, List<Zametka>>>();
                if (desereolize != null)
                {
                    if (Json.desereolize<Dictionary<string, List<Zametka>>>().Keys.Contains(datepiker.SelectedDate.ToString()))
                    {

                        change.Visibility = Visibility.Visible;
                        List<Zametka> next = desereolize[datepiker.SelectedDate.ToString()];
                        List<string> names = new List<string>();

                        foreach (Zametka note in next)
                        {
                            names.Add(note.name);
                        }
                        Allzametki.ItemsSource = names;
                    }
                    else
                    {

                        change.Visibility = Visibility.Hidden;
                        Allzametki.ItemsSource = null;
                        Name_note.Text = null;
                        Discrip_note.Text = null;

                    }
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void Allzametki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var deserialize = Json.desereolize<Dictionary<string, List<Zametka>>>()[datepiker.SelectedDate.ToString()];

            if (deserialize.Count() != 0)
            {
                try
                {
                    string name = Allzametki.Items[Allzametki.SelectedIndex].ToString();
                    string date = datepiker.SelectedDate.ToString();
                    List<Zametka> desdate = Json.desereolize<Dictionary<string, List<Zametka>>>()[date];
                    Discrip_note.Text = (desdate[Allzametki.SelectedIndex].descrption.ToString());
                    Name_note.Text = (desdate[Allzametki.SelectedIndex].name.ToString());
                }
                catch (Exception)
                {
                    
                }
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (Discrip_note.Text != "" && Name_note.Text != "")
            {
                var desereoolize = Json.desereolize<Dictionary<string, List<Zametka>>>();

                foreach (Zametka name in desereoolize[datepiker.SelectedDate.ToString()].ToList())
                {
                    if (name.name == Name_note.Text)
                    {
                        desereoolize[datepiker.SelectedDate.ToString()].Remove(desereoolize[datepiker.SelectedDate.ToString()][desereoolize[datepiker.SelectedDate.ToString()].IndexOf(name)]);

                        Json.sereolize(desereoolize);
                        var des1 = Json.desereolize<Dictionary<string, List<Zametka>>>();

                        if (des1 != null)
                        {
                            if (desereoolize.Keys.Contains(datepiker.SelectedDate.ToString()))
                            {
                                List<Zametka> next = des1[datepiker.SelectedDate.ToString()];
                                List<string> names = new List<string>();

                                foreach (Zametka n in next)
                                {
                                    names.Add(n.name);
                                }

                                Allzametki.ItemsSource = names;
                            }
                        }
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Discrip_note.Text != "" && Name_note.Text != "")
            {
                var desereolize = Json.desereolize<Dictionary<string, List<Zametka>>>();
                List<string> names = new List<string>();

                if (desereolize.Count != 0 && desereolize.ContainsKey(datepiker.SelectedDate.ToString()))
                {
                    foreach (Zametka name in desereolize[datepiker.SelectedDate.ToString()])
                    {
                        names.Add(name.name);
                    }
                    if (desereolize.ContainsKey(datepiker.SelectedDate.ToString()) && !names.Contains(Name_note.Text))
                    {
                        Zametka note = new Zametka();
                        note.descrption = Discrip_note.Text;
                        note.name = Name_note.Text;
                        note.TIme_disk = datepiker.SelectedDate.ToString();
                        desereolize[datepiker.SelectedDate.ToString()].Add(note);
                        Json.sereolize(desereolize);
                    }
                }
                else if (desereolize.Count == 0 || !desereolize.ContainsKey(datepiker.SelectedDate.ToString()))
                {
                    Zametka newNote = new Zametka();
                    newNote.descrption = Discrip_note.Text;
                    newNote.name = Name_note.Text;
                    newNote.TIme_disk = datepiker.SelectedDate.ToString();

                    List<Zametka> newDate = new List<Zametka>();
                    newDate.Add(newNote);
                    desereolize.Add(newNote.TIme_disk, newDate);
                    Json.sereolize(desereolize);
                }
                Allzametki.ItemsSource = desereolize[datepiker.SelectedDate.ToString()];
                Discrip_note.Text = "";
                Name_note.Text = "";
                change.Visibility = Visibility.Visible;
                var des1 = Json.desereolize<Dictionary<string, List<Zametka>>>();
                if (des1 != null)
                {
                    if (desereolize.Keys.Contains(datepiker.SelectedDate.ToString()))
                    {
                        List<Zametka> next = des1[datepiker.SelectedDate.ToString()];
                        List<string> names1 = new List<string>();

                        foreach (Zametka n in next)
                        {
                            names1.Add(n.name);
                        }

                        Allzametki.ItemsSource = names1;
                    }
                }

            }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            
            if (Discrip_note.Text != "" && Name_note.Text != "")
            {
                
                var name1241 = Allzametki.SelectedItem.ToString();
                var desereoolize = Json.desereolize<Dictionary<string, List<Zametka>>>();
                foreach (var item in desereoolize)
                {
                    if (item.Key.ToString() == datepiker.SelectedDate.ToString())
                    {
                        MessageBox.Show(name1241);
                        foreach (var item1 in item.Value)
                        {
                            if (item1.name == Allzametki.SelectedItem.ToString())
                            {
                                MessageBox.Show(name1241);
                                item1.name = Name_note.Text;
                                item1.descrption = Discrip_note.Text;
                                Json.sereolize(desereoolize);
                                Json.sereolize(desereoolize);

                                Name_note.Text = "";
                                Discrip_note.Text = "";

                                var desereoolize2 = Json.desereolize<Dictionary<string, List<Zametka>>>();

                                if (desereoolize2 != null)
                                {
                                    if (desereoolize.Keys.Contains(datepiker.SelectedDate.ToString()))
                                    {
                                        List<Zametka> next = desereoolize2[datepiker.SelectedDate.ToString()];
                                        List<string> names = new List<string>();

                                        foreach (Zametka n in next)
                                        {
                                            names.Add(n.name);
                                        }

                                        Allzametki.ItemsSource = names;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }



    internal class Zametka
    {
        public string name;
        public string descrption;
        public string TIme_disk;
    }
    internal class Json
    {
        public static string file = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\zametki.json";

        public static void sereolize<T>(T znachenie)
        {
            try
            {
                string json = JsonConvert.SerializeObject(znachenie);
                File.WriteAllText(file, json);
            }
            catch (Exception)
            {

            }
        }

        public static T desereolize<T>()
        {
            Debug.WriteLine(file);
            string json = File.ReadAllText(file);
            T notes = JsonConvert.DeserializeObject<T>(json);
            return notes;
        }
    }
}
