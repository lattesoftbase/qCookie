using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
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
using static System.Net.Mime.MediaTypeNames;

namespace qCookie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private long CookieCount = 0;
        private long FactoryCount = 0;
        private long CookieBoost = 2;
        private long FactoryPrice = 1;
        private long AutoClickPrice = 1;
        private long AutoClickers;
        private long XP = 0;

        string save;
        string[] save_ls;

        async private void checkSave(object sender, RoutedEventArgs e)
        {
            using (FileStream stream = new FileStream("save.txt", FileMode.OpenOrCreate))
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                save = Encoding.Default.GetString(buffer);

                if (save.Contains("/"))
                {
                    save_ls = save.Split("/");
                    XP = Convert.ToInt64(save_ls[0]);
                    CookieCount = Convert.ToInt64(save_ls[1]);
                    FactoryCount = Convert.ToInt64(save_ls[2]);
                    FactoryPrice = Convert.ToInt64(save_ls[3]);
                    CookieBoost = Convert.ToInt64(save_ls[4]);
                    cookieBlock.Text = $"{CookieCount.ToString()} cookies";
                    factoryBlock.Text = $"{FactoryCount.ToString()} factories";
                    factoryPriceBlock.Text = $"{FactoryPrice.ToString()} cookies";
                    XpBlock.Text = $"{XP.ToString()} XP!";
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CookieHandler(object sender, RoutedEventArgs e)
        {
            CookieCount += CookieBoost;
            cookieBlock.Text = $"{CookieCount.ToString()} cookies!";
        }

        private void FactoryHandler(object sender, RoutedEventArgs e)
        {
            if (CookieCount > FactoryPrice)
            {
                FactoryCount += 1;
                CookieCount -= FactoryPrice;
                FactoryPrice *= 3;
                CookieBoost *= 2;
                cookieBlock.Text = $"{CookieCount.ToString()} cookies";
                factoryBlock.Text = $"{FactoryCount.ToString()} factories!";
                factoryPriceBlock.Text = $"{FactoryPrice.ToString()} price cookies!";
                XP += 500;
                XpBlock.Text = $"{XP.ToString()} XP!";
            }
            else
            {
                MessageBox.Show("Недостаточно печенек!");
            }
        }

        async private void SaveHandler(object sender, RoutedEventArgs e)
        {
            using (FileStream stream = new FileStream("save.txt", FileMode.OpenOrCreate))
            {
                byte[] separator = Encoding.Default.GetBytes("/");
                byte[] buffer = Encoding.Default.GetBytes(XP.ToString());
                byte[] buffer1 = Encoding.Default.GetBytes(CookieCount.ToString());
                byte[] buffer2 = Encoding.Default.GetBytes(FactoryCount.ToString());
                byte[] buffer3 = Encoding.Default.GetBytes(FactoryPrice.ToString());
                byte[] buffer4 = Encoding.Default.GetBytes(CookieBoost.ToString());
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.WriteAsync(separator, 0, separator.Length);
                await stream.WriteAsync(buffer1, 0, buffer1.Length);
                await stream.WriteAsync(separator, 0, separator.Length);
                await stream.WriteAsync(buffer2, 0, buffer2.Length);
                await stream.WriteAsync(separator, 0, separator.Length);
                await stream.WriteAsync(buffer3, 0, buffer3.Length);
                await stream.WriteAsync(separator, 0, separator.Length);
                await stream.WriteAsync(buffer4, 0, buffer4.Length);
            }
        }
    }
}
