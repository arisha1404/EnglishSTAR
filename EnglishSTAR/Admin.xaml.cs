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

namespace EnglishSTAR
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        List<Service> ServiceList = BD.LE.Service.ToList();
        public Admin()
        {
            InitializeComponent();
            DGServises.ItemsSource = ServiceList;
        }
        int i = -1;

        private void StackPanel_Initialized(object sender, EventArgs e)
        {
            if (i < ServiceList.Count)
            {
                StackPanel SP = (StackPanel)sender;
                Service SE = ServiceList[i];
                if (SE.Discount !=0)
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
            MessageBox.Show(S.Title);
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
                TB.Text = " ";
            }
            else
            {
                int old_cost = Convert.ToInt32(SE.Cost);
                TB.TextDecorations = TextDecorations.Strikethrough;
                TB.Text = Convert.ToString(old_cost);
            }

        }
    }
}
