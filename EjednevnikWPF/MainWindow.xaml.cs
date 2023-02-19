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
            day.SelectedDate = DateTime.Today;
        }

        private void select_date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var desereolize = Json.desereolize();
            if (desereolize != null)
            {
                if (desereolize.Keys.Contains(day.SelectedDate.ToString()))
                {
                    List<Zametka> next = desereolize[day.SelectedDate.ToString()];
                    List<string> names = new List<string>();

                    foreach (Zametka note in next)
                    {
                        names.Add(note.name);
                    }
                    Allzametki.ItemsSource = names;
                }
                else
                {
                    Allzametki.ItemsSource = null;
                    Name_discrip.Text = "";
                    Discrip.Text = "";
                }
            }
        }

        private void Allzametki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var deserialize = Json.desereolize()[day.SelectedDate.ToString()];

            if (deserialize.Count() != 0)
            {
                try
                {
                    string name = Allzametki.Items[Allzametki.SelectedIndex].ToString();
                    string date = day.SelectedDate.ToString();
                    List<Zametka> des = Json.desereolize()[date];

                    foreach (Zametka newNote in des)
                    {
                        if (newNote.name == name)
                        {
                            Discrip.Text = newNote.descrption;
                            Name_discrip.Text = newNote.name;
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (Discrip.Text != "" && Name_discrip.Text != "")
            {
                var desereoolize = Json.desereolize();

                foreach (Zametka name in desereoolize[day.SelectedDate.ToString()].ToList())
                {
                    if (name.name == Name_discrip.Text)
                    {
                        desereoolize[day.SelectedDate.ToString()].Remove(desereoolize[day.SelectedDate.ToString()][desereoolize[day.SelectedDate.ToString()].IndexOf(name)]);

                        Json.sereolize(desereoolize);

                        Name_discrip.Text = "";
                        Discrip.Text = "";

                        var des1 = Json.desereolize();

                        if (des1 != null)
                        {
                            if (desereoolize.Keys.Contains(day.SelectedDate.ToString()))
                            {
                                List<Zametka> next = des1[day.SelectedDate.ToString()];
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
            if (Discrip.Text != null && Name_discrip.Text != null)
            {
                var desereolize = Json.desereolize();
                List<string> names = new List<string>();

                if (desereolize.Count != 0 && desereolize.ContainsKey(day.SelectedDate.ToString()))
                {
                    foreach (Zametka name in desereolize[day.SelectedDate.ToString()])
                    {
                        names.Add(name.name);
                    }
                    if (desereolize.ContainsKey(day.SelectedDate.ToString()) && !names.Contains(Name_discrip.Text))
                    {
                        Zametka note = new Zametka();
                        note.descrption = Discrip.Text;
                        note.name = Name_discrip.Text;
                        note.TIme_disk = day.SelectedDate.ToString();
                        desereolize[day.SelectedDate.ToString()].Add(note);
                        Json.sereolize(desereolize);
                    }
                }
                else if (desereolize.Count == 0 || !desereolize.ContainsKey(day.SelectedDate.ToString()))
                {
                    Zametka newNote = new Zametka();
                    newNote.descrption = Discrip.Text;
                    newNote.name = Name_discrip.Text;
                    newNote.TIme_disk = day.SelectedDate.ToString();

                    List<Zametka> newDate = new List<Zametka>();
                    newDate.Add(newNote);
                    desereolize.Add(newNote.TIme_disk, newDate);
                    Json.sereolize(desereolize);
                }
                Allzametki.ItemsSource = desereolize[day.SelectedDate.ToString()];
                Discrip.Text = "";
                Name_discrip.Text = "";

                var des1 = Json.desereolize();
                if (des1 != null)
                {
                    if (desereolize.Keys.Contains(day.SelectedDate.ToString()))
                    {
                        List<Zametka> next = des1[day.SelectedDate.ToString()];
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
            if (Discrip.Text != "" && Name_discrip.Text != "")
            {
                var desereoolize = Json.desereolize();
                var name1241 = Allzametki.SelectedItem.ToString();


                MessageBox.Show(name1241);

                foreach (Zametka name in desereoolize[day.SelectedDate.ToString()].ToList())
                {
                    if (name.name == name1241)
                    {
                        desereoolize[day.SelectedDate.ToString()].Remove(desereoolize[day.SelectedDate.ToString()][desereoolize[day.SelectedDate.ToString()].IndexOf(name)]);
                        Zametka newNote = new Zametka();
                        newNote.name = Name_discrip.Text;
                        newNote.descrption = Discrip.Text;
                        newNote.TIme_disk = day.SelectedDate.ToString();
                        desereoolize[day.SelectedDate.ToString()].Add(newNote);
                        Json.sereolize(desereoolize);

                        Name_discrip.Text = "";
                        Discrip.Text = "";

                        var desereoolize2 = Json.desereolize();

                        if (desereoolize2 != null)
                        {
                            if (desereoolize.Keys.Contains(day.SelectedDate.ToString()))
                            {
                                List<Zametka> next = desereoolize2[day.SelectedDate.ToString()];
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
    internal class Zametka
    {
        public string name;
        public string descrption;
        public string TIme_disk;
    }
    internal class Json
    {
        public static string file = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\zametki.json";

        public static void sereolize(Dictionary<string, List<Zametka>> obj)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj);
                File.WriteAllText(file, json);
            }
            catch (Exception)
            {

            }
        }

        public static Dictionary<string, List<Zametka>> desereolize()
        {
            Debug.WriteLine(file);
            string json = File.ReadAllText(file);
            Dictionary<string, List<Zametka>> notes = JsonConvert.DeserializeObject<Dictionary<string, List<Zametka>>>(json);

            return notes;
        }
    }
}
