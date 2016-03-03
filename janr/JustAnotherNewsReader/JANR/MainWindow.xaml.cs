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
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;

namespace JANR
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            InitDevice();
            InitTemplate();
            TextBlockDescription.Text = "使用方法：\n\n1.登录新闻发布者账号，（当前默认账号为Admin，密码为admin）\n2.（可选）将新闻链接由浏览器拖入空白处，程序将自动解析新闻页面的标题，内容，并将新闻图片上传至服务器（需登录进行）。\n3.输入，修改新闻内容及标题，插入图片并上传至服务器，实时在预览框中查看。\n4.点击提交，上传新闻至服务器\n\n中国软件杯参赛作品\n万事屋制作团队 制作";
        }
        public void InitDevice()
        {
            ComboBoxPresetDevice.Items.Add(new DeviceClass() { Name = "Google Nexus S", Width = 480, Height = 800, PixelRatio = 1.5, Margin_Left = 1, Margin_Top = 75, Margin_Right = 1, Margin_Bottom = 1, UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.4; en-us; Nexus S Build/GRJ22) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1", ScreenImage = "images/Google Nexus S (480_800).png" });
            ComboBoxPresetDevice.Items.Add(new DeviceClass() { Name = "Google Nexus 5", Width = 1080, Height = 1920, PixelRatio = 3, Margin_Left = 1, Margin_Top = 88, Margin_Right = 1, Margin_Bottom = 1, UserAgent = "Mozilla/5.0 (Linux; Android 4.2.1; en-us; Nexus 5 Build/JOP40D) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19", ScreenImage = "images/Google Nexus S (480_800).png" });
            ComboBoxPresetDevice.Items.Add(new DeviceClass() { Name = "Apple iPhone 5", Width = 640, Height = 1136, PixelRatio = 2, Margin_Left = 1, Margin_Top = 80, Margin_Right = 1, Margin_Bottom = 1, UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 7_0 like Mac OS X; en-us) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A465 Safari/9537.53", ScreenImage = "images/Google Nexus S (480_800).png" });
            ComboBoxPresetDevice.Items.Add(new DeviceClass() { Name = "Nokia Lumia 900", Width = 480, Height = 800, PixelRatio = 1.5, Margin_Left = 1, Margin_Top = 80, Margin_Right = 1, Margin_Bottom = 1, UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows Phone 8.0; Trident/6.0; IEMobile/10.0; ARM; Touch; NOKIA; Lumia 820)", ScreenImage = "images/Google Nexus S (480_800).png" });
            ComboBoxPresetDevice.SelectedIndex = 0;
        }
        public string NewsTemplate { get; set; }
        public void InitTemplate()
        {
            NewsTemplate = System.IO.File.ReadAllText("NewsTemplate.html");
        }
        public void InitWebBrowser()
        {
            //WebBrowserDevice.Navigate("http://m.baidu.com", "", new byte[] { }, String.Format("User-Agent: {0}", NowDevice.UserAgent));
        }
        protected DeviceClass _NowDevice = null;
        public DeviceClass NowDevice
        {
            get { return this._NowDevice; }
            set
            {
                this._NowDevice = value;
                DeviceClass device = value;
                if (device != null)
                {
                    TextBoxDeviceSize.Text = String.Format("{0},{1}", device.Width, device.Height);
                    TextBoxDevicePixelRatio.Text = String.Format("{0}", device.PixelRatio);
                    TextBoxDeviceMargin.Text = String.Format("{0},{1},{2},{3}", device.Margin_Left, device.Margin_Top, device.Margin_Right, device.Margin_Bottom);
                    ResizeDevice();
                }
            }
        }
        private void ComboBoxPresetDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxPresetDevice.SelectedItem != null)
            {
                DeviceClass device = ComboBoxPresetDevice.SelectedItem as DeviceClass;
                NowDevice = device;
            }
        }
        public void ResizeDevice()
        {
            BorderWebBrowserDevice.Width = NowDevice.Width / NowDevice.PixelRatio;
            BorderWebBrowserDevice.Height = NowDevice.Height / NowDevice.PixelRatio;
            WebBrowserDevice.Margin = new Thickness(NowDevice.Margin_Left, NowDevice.Margin_Top, NowDevice.Margin_Right, NowDevice.Margin_Bottom);
            try
            {
                BorderWebBrowserDevice.Background = new ImageBrush(new BitmapImage(new Uri(NowDevice.ScreenImage,UriKind.Relative)));
            }
            catch (Exception) { }
        }

        JanrSettingsClass settings = new JanrSettingsClass();
        HttpClient webclient = new HttpClient();
        SHA1 sha = new SHA1CryptoServiceProvider();
        Encoding enc = new ASCIIEncoding();
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string UserName = TextBoxUserName.Text;
            string Password = TextBoxPassword.Text;
            for (int i = 0; i < 500; i++)
            {
                byte[] BytePassword = enc.GetBytes(Password);
                BytePassword = sha.ComputeHash(BytePassword);
                Password = BitConverter.ToString(BytePassword).Replace("-", "").ToLower();
            }
            string postdata = String.Format("user_name={0}&user_password={1}", UserName, Password); //post数据

            webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            string result = webclient.UploadString(String.Format("{0}Login.php", settings.Server), postdata);
            if (result != "Success")
            {
                MessageBox.Show(result);
                return;
            }
            TextBlockUserName.Text = UserName;
            PanelBeforeLogin.Visibility = Visibility.Collapsed;
            PanelAfterLogin.Visibility = Visibility.Visible;

        }

        private void WebBrowserDevice_LoadCompleted(object sender, NavigationEventArgs e)
        {
            mshtml.HTMLDocument dom = (mshtml.HTMLDocument)WebBrowserDevice.Document; //定义HTML
            dom.documentElement.style.overflow = "hidden";    //隐藏浏览器的滚动条 
            dom.body.setAttribute("scroll", "no");            //禁用浏览器的滚动条
        }

        private void EditorDevice_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string document = NewsTemplate.Replace("%BODY%", EditorDevice.Text);
                document = document.Replace("src=\"", "src=\"" + settings.Server);
                WebBrowserDevice.NavigateToString(document);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void WebBrowserDevice_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (fiComWebBrowser == null)
                return;
            object objComWebBrowser = fiComWebBrowser.GetValue(WebBrowserDevice);
            if (objComWebBrowser == null)
                return;
            objComWebBrowser.GetType().InvokeMember("Silent", System.Reflection.BindingFlags.SetProperty, null, objComWebBrowser, new object[] { true });

        }

        private void ButtonFetchSource_Click(object sender, RoutedEventArgs e)
        {
            FetchUrl(TextBoxNewsSourceUrl.Text);
        }
        public void FetchUrl(string url)
        {
            SingleNewsClass snc = new SingleNewsClass(url);
            TextBoxNewsTitle.Text = snc.Title;
            TextBoxNewsOriginalTime.Text = snc.Time.ToString("yyyy-MM-dd HH:mm:ss");
            string plain = ClearFormat(snc.Content, true);
            if (plain.Length > 200)
                TextBoxNewsIntro.Text = plain.Substring(0, 200);
            else
                TextBoxNewsIntro.Text = plain;
            snc.Content = snc.Content.Replace("<p", "\n<p");
            snc.Content = snc.Content.Replace("<img", "\n<img sytle=\"width:100%\"");
            EditorDevice.Text =String.Format("<h1>{0}</h1>\n{1}",snc.Title ,snc.Content);
            for (int i = 0; i < snc.ImageUrl.Count; i++)
            {
                string remoteUrl=snc.ImageUrl[i];
                string type=System.IO.Path.GetExtension(remoteUrl);
                string LocalFileName="images/"+DateTime.Now.ToString("yyyyMMddHHmmss")+i.ToString("000")+type;
                webclient.DownloadFile(remoteUrl, LocalFileName);
                string filename=UploadImage(LocalFileName);
                if (filename == "")
                    break;
                EditorDevice.Text=EditorDevice.Text.Replace("@img/" + i.ToString("000"), filename);
            }
        }

        private void Window_Drop_1(object sender, DragEventArgs e)
        {
            String url = e.Data.GetData(typeof(String)).ToString();
            if (url.StartsWith("http"))
            {
                TextBoxNewsSourceUrl.Text = url;
                FetchUrl(url);
            }
        }
        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string Title = TextBoxNewsTitle.Text;
            string Time=TextBoxNewsOriginalTime.Text;
            string Content = EditorDevice.Text;
            string Intro=TextBoxNewsIntro.Text;
            string Catagory = "1";
            foreach (RadioButton rb in RadioButtonCatagory)
            {
                if (rb.IsChecked??false)
                    Catagory = rb.Tag.ToString();
            }
            string postdata = String.Format("news_title={0}&news_content={1}&news_oritime={2}&news_intro={3}&news_tag=&news_thumb=&news_category={0}", Title, Content, Time, Intro,Catagory);

            webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            string result = webclient.UploadString(String.Format("{0}AddNews.php", settings.Server), postdata);
                MessageBox.Show(result);
        }

        private void ButtonClearFormat_Click(object sender, RoutedEventArgs e)
        {
            EditorDevice.Text = ClearFormat(EditorDevice.Text,false);
        }
        public string ClearFormat(string text,bool full)
        {
            List<string> UsefulPrefix =new List<string>() { "p", "/p", "h1", "/h1", "br", "img", "/img" };
            if (full)
                UsefulPrefix.Clear();
            List<string> UselessString = new List<string>();
            Regex reg = new Regex(@"\<([^>]*)\>");
            MatchCollection mc = reg.Matches(text);
            foreach (Match m in mc)
            {
                string nodeattr = m.Result("$1");
                string lowernodeattr = nodeattr.ToLower();
                bool flag = true;
                for (int i = 0; i < UsefulPrefix.Count; i++)
                    if (lowernodeattr.StartsWith(UsefulPrefix[i]))
                    {
                        flag = false;
                        break;
                    }
                if (flag)
                    text = text.Replace(String.Format(@"<{0}>", nodeattr), "");
            }
            return text;
        }

        private void ButtonUploadImage_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "jpeg picture *.jpg|*.jpg|png picture *.png|*.png|gif picture *.gif|*.gif";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = UploadImage(ofd.FileName);
                string imagelabel=String.Format("<img style=\"width:100%\" src=\"{0}\"  />",filename);
                EditorDevice.Text=EditorDevice.Text.Insert(EditorDevice.SelectionStart, imagelabel);
            }
        }
        public string UploadImage(string LocalFileName)
        {
            byte[] respondbytes = webclient.UploadFile(String.Format("{0}UploadImage.php", settings.Server), LocalFileName);
            UTF8Encoding enc = new UTF8Encoding();
            string respond = enc.GetString(respondbytes);
            if (!respond.StartsWith("Success"))
            {
                MessageBox.Show(respond);
                return "";
            }
            Regex regex = new Regex("<File>(.*)</File>");
            string filename = regex.Match(respond).Result("$1");
            return filename;
        }


        private void TextBoxDevice_LostFocus(object sender, RoutedEventArgs e)
        {
            string[] DeviceSize = TextBoxDeviceSize.Text.Split(',');
            string DevicePixelRatio = TextBoxDevicePixelRatio.Text;
            string[] DeviceMargin = TextBoxDeviceMargin.Text.Split(',');
            if (DeviceSize.Length >= 2 && DeviceMargin.Length >= 4)
            {
                int Width,Height,Left,Right,Top,Bottom;
                double PixelRatio;
                if (int.TryParse(DeviceSize[0], out Width)
                    && int.TryParse(DeviceSize[1], out Height)
                    && double.TryParse(DevicePixelRatio, out PixelRatio)
                    && int.TryParse(DeviceMargin[0], out Left)
                    && int.TryParse(DeviceMargin[1], out Top)
                    && int.TryParse(DeviceMargin[2], out Right)
                    && int.TryParse(DeviceMargin[3], out Bottom))
                {
                    if (!(NowDevice.Width == Width
                        && NowDevice.Height == Height
                        && NowDevice.PixelRatio == PixelRatio
                        && NowDevice.Margin_Left == Left
                        && NowDevice.Margin_Top == Top
                        && NowDevice.Margin_Right == Right
                        && NowDevice.Margin_Bottom == Bottom))
                    {
                        NowDevice = new DeviceClass()
                        {
                            Name="Custom Device",
                            Width=Width,
                            Height=Height,
                            PixelRatio=PixelRatio,
                            Margin_Left=Left,
                            Margin_Top=Top,
                            Margin_Right=Right,
                            Margin_Bottom=Bottom
                        };
                        ComboBoxPresetDevice.SelectedIndex = -1;
                    }
                }
            }
            ResizeDevice();
        }
        List<RadioButton> RadioButtonCatagory = new List<RadioButton>();
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            string catagoryXml=webclient.DownloadString(String.Format("{0}GetCatagories.php",settings.Server));
            System.Xml.XmlDocument xml =new System.Xml.XmlDocument();
            xml.LoadXml(catagoryXml);
            XmlNodeList nodelist= xml.SelectNodes("/xml/Catatories/Catagory");
            for (int i = 0; i < nodelist.Count; i++)
            {
                RadioButton rb = new RadioButton();
                rb.Tag=int.Parse(nodelist[i]["ID"].InnerText);
                rb.Content = nodelist[i]["Display"].InnerText;
                rb.GroupName = "Catagory";
                RadioButtonCatagory.Add(rb);
                StackPanelCatagory.Children.Add(rb);
            }
        }

        private void ButtonAdminUsers_Click(object sender, RoutedEventArgs e)
        {
            WindowWeb ww = new WindowWeb();
            ww.Navigate(String.Format("{0}AdminUsers.php", settings.Server));
            ww.ShowDialog();
        }

        private void ButtonAdminCatagories_Click(object sender, RoutedEventArgs e)
        {
            WindowWeb ww = new WindowWeb();
            ww.Navigate(String.Format("{0}AdminUsers.php", settings.Server));
            ww.ShowDialog();
        }

        private void ButtonAdminNews_Click(object sender, RoutedEventArgs e)
        {
            WindowWeb ww = new WindowWeb();
            ww.Navigate(String.Format("{0}AdminNews.php", settings.Server));
            ww.ShowDialog();
        }
    }
}
