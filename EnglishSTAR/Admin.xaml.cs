using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.Data.Entity.Migrations;
using System.Text.RegularExpressions;

namespace EnglishSTAR
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {

        List<Service> ServiceList1 = BD.LE.Service.ToList();
        List<Service> ServiceList = new List<Service>();
        public Admin()
        {
            InitializeComponent();
            ServiceList = ServiceList1;
            DGServises.ItemsSource = ServiceList;
            CBPeople.ItemsSource = BD.LE.Client.ToList();
            CBPeople.SelectedValuePath = "ID";
            CBPeople.DisplayMemberPath = "People";
        }
        int i = -1;

        private void StackPanel_Initialized(object sender, EventArgs e)
        {
            if (i < ServiceList.Count)
            {
                StackPanel SP = (StackPanel)sender;
                Service SE = ServiceList[i];
                if (SE.Discount != 0)
                {
                    SP.Background = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                }
            }

        }

        private void MediaElement_Initialized(object sender, EventArgs e)
        {
            if (i < ServiceList.Count)
            {
                i++;
                MediaElement ME = (MediaElement)sender;
                Service SE = ServiceList[i];
                Uri U = new Uri(SE.MainImagePath, UriKind.RelativeOrAbsolute);
                ME.Source = U;
            }
        }
        private void TextBlock_Initialized(object sender, EventArgs e)
        {
            if (i < ServiceList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service SE = ServiceList[i];
                TB.Text = SE.Title;
            }
        }

        private void skidka_Initialized(object sender, EventArgs e)
        {
            if (i < ServiceList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service SE = ServiceList[i];
                if (SE.Discount == 0)
                {

                    int old_cost = Convert.ToInt32(SE.Cost);
                    int time = Convert.ToInt32(SE.DurationInSeconds / 60);
                    TB.Text = Convert.ToString(" " + old_cost + " рублей за " + time + " минут");
                }
                else
                {
                    TB.Text = Convert.ToString("* скидка " + SE.Discount * 100 + "%");
                }

            }
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            Button BtnRed = (Button)sender;
            int ind = Convert.ToInt32(BtnRed.Uid);
            Service S = ServiceList[ind];
            DGServises.Visibility = Visibility.Collapsed;
            forma.Visibility = Visibility.Visible;
            dob.Visibility = Visibility.Collapsed;
            Bsorti.Visibility = Visibility.Collapsed;
            dann.Visibility = Visibility.Collapsed;
            yslugi.Visibility = Visibility.Collapsed;
            zap.Visibility = Visibility.Collapsed;
            reda.Visibility = Visibility.Visible;
            new_dob.Visibility = Visibility.Collapsed;
            forma_dob.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Collapsed;
            id_f.Text = Convert.ToString(S.ID);
            name_f.Text = S.Title;
            cost_f.Text = Convert.ToInt32(S.Cost) + "";
            dlit_f.Text = Convert.ToInt32(S.DurationInSeconds / 60) + "";
            opis_f.Text = S.Description;
            skid_f.Text = Convert.ToDouble(S.Discount * 100) + "";
        }

        private void newcost_Initialized(object sender, EventArgs e)
        {
            if (i < ServiceList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service SE = ServiceList[i];
                int discount = Convert.ToInt32(SE.Discount * 100);
                int cost = Convert.ToInt32(SE.Cost);
                int cost_dis = cost - (cost / 100 * discount);
                int time = Convert.ToInt32(SE.DurationInSeconds / 60);
                if (SE.Discount == 0)
                {
                    TB.Visibility = Visibility.Collapsed;
                }
                else
                {

                    TB.Text = Convert.ToString(" " + cost_dis + " рублей за " + time + " минут");
                }
            }
        }

        private void red_Initialized(object sender, EventArgs e)
        {
            Button BtnRed = (Button)sender;
            if (BtnRed != null)
            {
                BtnRed.Uid = Convert.ToString(i);
            }

        }

        private void cost_Initialized(object sender, EventArgs e)
        {
            TextBlock TB = (TextBlock)sender;
            Service SE = ServiceList[i];
            if (SE.Discount == 0)
            {
                TB.Visibility = Visibility.Collapsed;
            }
            else
            {
                int old_cost = Convert.ToInt32(SE.Cost);
                TB.TextDecorations = TextDecorations.Strikethrough;
                TB.Text = Convert.ToString(old_cost);
            }

        }

        private void del_Initialized(object sender, EventArgs e)
        {
            Button BtnDel = (Button)sender;
            if (BtnDel != null)
            {
                BtnDel.Uid = Convert.ToString(i);
            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            Button BtnDel = (Button)sender;
            int ind = Convert.ToInt32(BtnDel.Uid);
            DialogResult dialogResult = (DialogResult)MessageBox.Show("Вы действительно хотите удалить данную запись?", "Удаление записи", (MessageBoxButton)MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Service S = ServiceList[ind];
                BD.LE.Service.Remove(S);
                MessageBox.Show("Запись удалена");
                BD.LE.SaveChanges();
                Frm.Mfrm.Navigate(new Admin());
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Запись не удалена");
            }

        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            DGServises.Visibility = Visibility.Visible;
            forma.Visibility = Visibility.Collapsed;
            dob.Visibility = Visibility.Collapsed;
            Bsorti.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Collapsed;
            dann.Visibility = Visibility.Collapsed;
            dobav_ff.Visibility = Visibility.Collapsed;
            yslugi.Visibility = Visibility.Visible;
            reda.Visibility = Visibility.Collapsed;
            final_dob.Visibility = Visibility.Collapsed;
            zap.Visibility = Visibility.Collapsed;
            new_dob.Visibility = Visibility.Collapsed;
            forma_dob.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Visible;
            Frm.Mfrm.Navigate(new Admin());
        }

        private void img_f_Initialized(object sender, EventArgs e)
        {
            Button imgf = (Button)sender;
            if (imgf != null)
            {
                imgf.Uid = Convert.ToString(i);
            }
        }

        private void img_f_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string path = OFD.FileName;
            int c = path.IndexOf('У');
            string len = path.Substring(c);
            foto_f.Text = len.ToString();
        }

        private void soxr_Initialized(object sender, EventArgs e)
        {
            Button Izm = (Button)sender;
            if (Izm != null)
            {
                Izm.Uid = Convert.ToString(i);
            }
        }
        private void soxr_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(dlit_f.Text) > 14400 || Convert.ToInt32(dlit_f.Text) < 0)
            {
                MessageBox.Show("Время не должно привышать 4 часов и не может быть отрицательным");
            }
            else if (Convert.ToInt32(dlit_f.Text) < 14400 || Convert.ToInt32(dlit_f.Text) > 0)
            {
                double skid = Convert.ToDouble(skid_f.Text) / 100;
                int time = Convert.ToInt32(dlit_f.Text) * 60;
                DialogResult dialogResult = (DialogResult)MessageBox.Show("Вы действительно хотите внести изменения в данную запись?", "Сохранение изменений записи", (MessageBoxButton)MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Service ServiceObject = new Service() { ID = Convert.ToInt32(id_f.Text), Title = name_f.Text, Cost = Convert.ToInt32(cost_f.Text), DurationInSeconds = time, Description = opis_f.Text, Discount = skid, MainImagePath = foto_f.Text };
                    BD.LE.Service.AddOrUpdate(ServiceObject);
                    BD.LE.SaveChanges();
                    MessageBox.Show("Изменения были сохранены");
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Изменения не были сохранены");
                }
            }
        }
        private void dob_Initialized(object sender, EventArgs e)
        {
            Button Dab = (Button)sender;
            if (Dab != null)
            {
                Dab.Uid = Convert.ToString(i);
            }
        }
        private void dob_Click(object sender, RoutedEventArgs e)
        {
            DGServises.Visibility = Visibility.Collapsed;
            forma.Visibility = Visibility.Collapsed;
            dobav_ff.Visibility = Visibility.Visible;
            yslugi.Visibility = Visibility.Collapsed;
            reda.Visibility = Visibility.Collapsed;
            zap.Visibility = Visibility.Visible;
            dob.Visibility = Visibility.Collapsed;
            Bsorti.Visibility = Visibility.Collapsed;
            dann.Visibility = Visibility.Collapsed;
            new_dob.Visibility = Visibility.Collapsed;
            forma_dob.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Collapsed;
        }

        private void img_ff_Initialized(object sender, EventArgs e)
        {
            Button imgff = (Button)sender;
            if (imgff != null)
            {
                imgff.Uid = Convert.ToString(i);
            }
        }

        private void img_ff_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string path = OFD.FileName;
            int c = path.IndexOf('У');
            string len = path.Substring(c);
            foto_ff.Text = len.ToString();
        }
        private void final_dob_Initialized(object sender, EventArgs e)
        {
            Button fin = (Button)sender;
            if (fin != null)
            {
                fin.Uid = Convert.ToString(i);
            }
        }
        private void final_dob_Click(object sender, RoutedEventArgs e)
        {
            Service SE = ServiceList[i];
            if (SE.Title == name_ff.Text)
            {
                MessageBox.Show("Такое название уже существует, поэтому придумайте новое");
            }
            if (Convert.ToInt32(dlit_ff.Text) > 14400 || Convert.ToInt32(dlit_ff.Text) < 0)
            {
                MessageBox.Show("Время не должно привышать 4 часов");
            }
            else if (Convert.ToInt32(dlit_ff.Text) < 14400 || Convert.ToInt32(dlit_ff.Text) > 0)
            {
                double skid = Convert.ToDouble(skid_ff.Text) / 100;
                int time = Convert.ToInt32(dlit_ff.Text) * 60;
                DialogResult dialogResult = (DialogResult)MessageBox.Show("Вы действительно хотите добавить новую запись?", "Запись успешно добавлена", (MessageBoxButton)MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Service ServiceObject = new Service() { Title = name_ff.Text, Cost = Convert.ToInt32(cost_ff.Text), DurationInSeconds = time, Description = opis_ff.Text, Discount = skid, MainImagePath = foto_ff.Text };
                    BD.LE.Service.Add(ServiceObject);
                    BD.LE.SaveChanges();
                    MessageBox.Show("Запись добавлена");
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Запись не добавлена");
                }
            }
        }

        private void naz_Click(object sender, RoutedEventArgs e)
        {
            DGServises.Visibility = Visibility.Visible;
            forma.Visibility = Visibility.Collapsed;
            yslugi.Visibility = Visibility.Visible;
            dobav_ff.Visibility = Visibility.Collapsed;
            dob.Visibility = Visibility.Visible;
            Bsorti.Visibility = Visibility.Visible;
            dann.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Collapsed;
            zap.Visibility = Visibility.Collapsed;
            reda.Visibility = Visibility.Collapsed;
            new_dob.Visibility = Visibility.Collapsed;
            forma_dob.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Visible;
        }

        private void dobav_Initialized(object sender, EventArgs e)
        {
            Button Bdobav = (Button)sender;
            if (Bdobav != null)
            {
                Bdobav.Uid = Convert.ToString(i);
            }
        }

        private void dobav_Click(object sender, RoutedEventArgs e)
        {
            Button Bdobav = (Button)sender;
            int ind = Convert.ToInt32(Bdobav.Uid);
            Service S = ServiceList[ind];
            DGServises.Visibility = Visibility.Collapsed;
            forma.Visibility = Visibility.Collapsed;
            yslugi.Visibility = Visibility.Collapsed;
            dobav_ff.Visibility = Visibility.Collapsed;
            dob.Visibility = Visibility.Collapsed;
            Bsorti.Visibility = Visibility.Collapsed;
            dann.Visibility = Visibility.Collapsed;
            zap.Visibility = Visibility.Collapsed;
            reda.Visibility = Visibility.Collapsed;
            new_dob.Visibility = Visibility.Visible;
            forma_dob.Visibility = Visibility.Visible;
            sort.Visibility = Visibility.Collapsed;
            nam_dob.Text = S.Title; ;
            dlit_dob.Text = Convert.ToInt32(S.DurationInSeconds / 60) + "";
        }

        private void naz_dobav_Click(object sender, RoutedEventArgs e)
        {
            DGServises.Visibility = Visibility.Visible;
            forma.Visibility = Visibility.Collapsed;
            yslugi.Visibility = Visibility.Visible;
            dobav_ff.Visibility = Visibility.Collapsed;
            dob.Visibility = Visibility.Visible;
            Bsorti.Visibility = Visibility.Visible;
            sort.Visibility = Visibility.Collapsed;
            dann.Visibility = Visibility.Collapsed;
            zap.Visibility = Visibility.Collapsed;
            reda.Visibility = Visibility.Collapsed;
            new_dob.Visibility = Visibility.Collapsed;
            forma_dob.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Collapsed;
        }

        DateTime DT;
        private void TBtime_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Regex r1 = new Regex("[0-1][0-9]:[0-5][0-9]");
                Regex r2 = new Regex("2[0-3]:[0-5][0-9]");
                if ((r1.IsMatch(TBtime.Text) || r2.IsMatch(TBtime.Text)) && TBtime.Text.Length == 5)
                {
                    MessageBox.Show(TBtime.Text);
                    TimeSpan TS = TimeSpan.Parse(TBtime.Text);
                    DT = Convert.ToDateTime(DP_dob.SelectedDate);
                    DT = DT.Add(TS);
                    if (DT > DateTime.Now)
                    {
                        MessageBox.Show(DT + "");
                    }
                    else
                    {
                        MessageBox.Show("Данная дата уже прошла");
                        soxr_dob.IsEnabled = false;
                    }
                }
                else
                {
                    if (TBtime.Text.Length >= 5)
                    {
                        MessageBox.Show("Время указано неверно");
                        zap.IsEnabled = false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Где-то была допущена ошибка");
                zap.IsEnabled = false;
            }
        }

        private void Bup_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServiceList.Sort((x, y) => x.Cost.CompareTo(y.Cost));
            DGServises.Items.Refresh();

        }

        private void Bdown_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServiceList.Sort((x, y) => x.Cost.CompareTo(y.Cost));
            ServiceList.Reverse();
            DGServises.Items.Refresh();

        }
        List<Service> ServiseListFilter = new List<Service>();
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            i = -1;
            for (int i = 0; i < ServiceList.Count; i++)
            {
                switch (Filter.SelectedIndex)
                {
                    case 0:
                        ServiceList = ServiceList1;
                        ServiseListFilter = ServiceList.Where(x => x.Discount < 0.05).ToList();
                        ServiceList = ServiseListFilter;
                        DGServises.ItemsSource = ServiceList;
                        Kol.Text = ServiceList.Count.ToString();
                        Ob_kol.Text = ServiceList1.Count.ToString();
                        break;
                    case 1:
                        ServiceList = ServiceList1;
                        ServiseListFilter = ServiceList.Where(x => (x.Discount >= 0.05) && (x.Discount < 0.15)).ToList();
                        ServiceList = ServiseListFilter;
                        DGServises.ItemsSource = ServiceList;
                        Kol.Text = ServiceList.Count.ToString();
                        Ob_kol.Text = ServiceList1.Count.ToString();
                        break;
                    case 2:
                        ServiceList = ServiceList1;
                        ServiseListFilter = ServiceList.Where(x => (x.Discount >= 0.15) && (x.Discount < 0.3)).ToList();
                        ServiceList = ServiseListFilter;
                        DGServises.ItemsSource = ServiceList;
                        Kol.Text = ServiceList.Count.ToString();
                        Ob_kol.Text = ServiceList1.Count.ToString();
                        break;
                    case 3:
                        ServiceList = ServiceList1;
                        ServiseListFilter = ServiceList.Where(x => (x.Discount >= 0.3) && (x.Discount < 0.7)).ToList();
                        ServiceList = ServiseListFilter;
                        DGServises.ItemsSource = ServiceList;
                        Kol.Text = ServiceList.Count.ToString();
                        Ob_kol.Text = ServiceList1.Count.ToString();
                        break;
                    case 4:
                        ServiceList = ServiceList1;
                        ServiseListFilter = ServiceList.Where(x => (x.Discount >= 0.7) && (x.Discount < 1)).ToList();
                        ServiceList = ServiseListFilter;
                        DGServises.ItemsSource = ServiceList;
                        Kol.Text = ServiceList.Count.ToString();
                        Ob_kol.Text = ServiceList1.Count.ToString();
                        break;
                    case 5:
                        ServiceList = ServiceList1;
                        ServiseListFilter = ServiceList.Where(x => (x.Discount >= 0) && (x.Discount <= 1)).ToList();
                        ServiceList = ServiseListFilter;
                        DGServises.ItemsSource = ServiceList;
                        Kol.Text = ServiceList.Count.ToString();
                        Ob_kol.Text = ServiceList1.Count.ToString();
                        break;
                }
            }
        }

        private void poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            i = -1;
            for (int i = 0; i < ServiceList.Count; i++)
            {
                if (poisk.Text != "")
                {
                    List<Service> ServiseListPoisk = new List<Service>();
                    ServiseListPoisk = ServiceList.Where(x => x.Title.Contains(poisk.Text)).ToList();
                    ServiceList = ServiseListPoisk;
                    DGServises.ItemsSource = ServiceList;
                    Kol.Text = ServiceList.Count.ToString();
                    Ob_kol.Text = ServiceList1.Count.ToString();
                }
                else
                {
                    if (ServiseListFilter.Count == 0)
                    {
                        ServiceList = ServiceList1;
                        DGServises.ItemsSource = ServiceList;
                    }
                    else
                    {
                        ServiceList = ServiseListFilter;
                        DGServises.ItemsSource = ServiceList;
                    }

                }
            }
        }

        private void Bsorti_Click(object sender, RoutedEventArgs e)
        {
            sort.Visibility = Visibility.Visible;
            dann.Visibility = Visibility.Visible;
        }
    }
}
