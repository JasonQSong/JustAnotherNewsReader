using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace JANR
{
    public class SingleNewsClass
    {
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string newsHtml { get; set; }
        public List<string> ImageUrl = new List<string>();
        /*public SingleNewsClass()
    {
        <img src="imagex.jpg"/>
                
        Images.Add("http://sina.com/img.jpg");
        <img src="@img/001"/>
        for(int i=0;i<ImageUrl.Count;i++);
    }*/


        public string getHtml(string url)
        {
            WebClient myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            string strWebData = Encoding.Default.GetString(myDataBuffer);
            Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)([^\"]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string webCharSet = charSetMatch.Groups[3].Value;
            webCharSet = Regex.Replace(webCharSet, @"[^a-zA-Z0-9\u4e00-\u9fa5\s\-]", "");
            //ContentList.Add("charset:"+webCharSet);
            strWebData = Encoding.GetEncoding(webCharSet).GetString(myDataBuffer);
            return strWebData;
        }

        public string getTitle(string newsHtml)
        {
            string title = "";
            //title = newsHtml;
            Regex r = new Regex(@"<h1[^>]*>(\s)*.*(\s)*</h1>");
            MatchCollection matches = r.Matches(newsHtml);
            foreach (Match match in matches)
            {
                string word = match.Value;
                Match charSetMatch = Regex.Match(word, @"<h1[^>]*>([^.]*)</h1>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                word = charSetMatch.Groups[1].Value;
                int index = match.Index;
                title = title + word;
            }
            return title;
        }

        public string getContent(string newsHtml)
        {
            string content = "";
            //title = newsHtml;
            //Regex r = new Regex(@"<[p|br][^>]*>(.*)</[p|br]>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex r = new Regex(@"<[p|b][^>]*>(\s)*(.*)</*[p|b][^>]*/*>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection matches = r.Matches(newsHtml);
            //MatchCollection matches = r.Matches("<br>adfsadf</br>");
            foreach (Match match in matches)
            {
                string word = match.Value;
                int index = match.Index;
                content = content + " " + word;
            }
            Regex r1 = new Regex(@"<[l][^>]*>(.*)</*[l][^>]*/*>");
            if (r1.IsMatch(content))
            {
                content = r1.Replace(content, "");
            }
            Regex r2 = new Regex(@"<p[^>]*><a[^>]*>(.*)</*a/*></p>");
            if (r2.IsMatch(content))
            {
                content = r2.Replace(content, "");
            }
            return content;
        }


        public List<string> getPic(string content)
        {
            //List<string> ImageUrl = new List<string>();
            string picUrl = "";
            //title = newsHtml;
            //Regex r = new Regex(@"<[p|br][^>]*>(.*)</[p|br]>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex r = new Regex(@"<img[^>]*alt[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection matches = r.Matches(content);
            int i = 0;
            int change = 0;
            foreach (Match match in matches)
            {
                string word = match.Value;
                int index = match.Index;
                Content = Content.Remove(index - change, word.Length);
                string tmp = "<img src=\"@img/" + i.ToString("000") + "\"/>";
                Content = Content.Insert(index - change, tmp);
                picUrl = picUrl + word;
                i++;
                change = change + word.Length - tmp.Length;
            }
            Regex r1 = new Regex("http://[^\"]*jpg", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection matches1 = r1.Matches(picUrl);
            picUrl = "";
            foreach (Match match in matches1)
            {
                string word = match.Value;
                int index = match.Index;
                ImageUrl.Add(word);
            }
            if (ImageUrl.Count == 0)
            {
                ImageUrl = getPic2(newsHtml);
                change = 0;
                for (i = 0; i < ImageUrl.Count; i++)
                {
                    string tmp = "<img src=\"@img/" + i.ToString("000") + "\"/>";
                    Content = Content.Insert(change, tmp);
                    change += tmp.Length;
                }
            }
            return ImageUrl;
        }

        public List<string> getPic2(string newsHtml)
        {
            //List<string> ImageUrl = new List<string>();
            string picUrl = "";
            //title = newsHtml;
            //Regex r = new Regex(@"<[p|br][^>]*>(.*)</[p|br]>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex rh = new Regex(@"<h1[^>]*>.*</h1>");
            MatchCollection matches = rh.Matches(newsHtml);
            int indexh = 0;
            foreach (Match match in matches)
            {
                string word = match.Value;
                Match charSetMatch = Regex.Match(word, @"<h1[^>]*>([^.]*)</h1>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                word = charSetMatch.Groups[1].Value;
                indexh = match.Index;
            }
            Regex r = new Regex(@"<[p|b][^>]*>(.*)</*[p|b][^>]*/*>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            matches = r.Matches(newsHtml);
            //MatchCollection matches = r.Matches("<br>adfsadf</br>");
            int indexc = newsHtml.Length;
            if (matches[0].Index != 0) indexc = matches[0].Index;
            int i = 0;
            while (indexc - indexh < 0)
            {
                if (i < matches.Count)
                {
                    indexc = matches[i].Index;
                    i++;
                }
            }
            string n = newsHtml.Substring(indexh, indexc - indexh);
            Regex r0 = new Regex(@"<img[^>]*alt[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            matches = r0.Matches(n);
            foreach (Match match in matches)
            {
                string word = match.Value;
                int index = match.Index;
                picUrl = picUrl + word;
            }
            Regex r1 = new Regex("http://[^\"]*jpg", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection matches1 = r1.Matches(picUrl);
            picUrl = "";
            foreach (Match match in matches1)
            {
                string word = match.Value;
                int index = match.Index;
                ImageUrl.Add(word);
            }
            return ImageUrl;
        }
        public SingleNewsClass()
        {
        }

        public SingleNewsClass(string url)
        {
            //string picUrls;
            ImageUrl = new List<string>();
            //List<string> ContentList = new List<string>();
            List<string> UrlList = new List<string>();
            UrlList.Add("http://news.qq.com/a/20140608/005255.htm");
            UrlList.Add("http://news.sina.com.cn/w/2014-06-09/050030319898.shtml");
            UrlList.Add("http://news.163.com/14/0609/11/9U9VV22K00014JB6.html#f=wlist");
            UrlList.Add("http://news.sohu.com/20140609/n400602585.shtml?adsid=1");
            UrlList.Add("http://news.ifeng.com/a/20140604/40585717_0.shtml");
            UrlList.Add("http://news.sohu.com/20140609/n400619616.shtml");
            newsHtml = "";
            try
            {
                if (url.Length < 3)
                {
                    int testNum = int.Parse(url);
                    newsHtml = getHtml(UrlList[testNum]);
                    //ContentList.Add("url: " + UrlList[testNum]);
                }
                else
                {
                    newsHtml = getHtml(url);
                    //ContentList.Add("url: " + url);
                }
            }
            catch { }
            //newsHtml = newsHtml.Replace("\n","");
            try { Title = getTitle(newsHtml); }
            catch { }
            try { Content = getContent(newsHtml); }
            catch { }
            try { ImageUrl = getPic(Content); }
            catch { }
            //string ImageUrlStr = string.Join("\r\n", ImageUrl.ToArray());
            try { Time = getTime(); }
            catch { }
            //str = string.Join("\r\n", ContentList.ToArray());
            //return output;
        }

        public string getTimeStr()
        {
            string time = "";
            Regex r = new Regex(@">(\s)*2[^<]*[0-2][0-9]:[0-6][0-9][^<]*<");
            MatchCollection matches = r.Matches(newsHtml);
            string word = matches[0].Value;
            int index = matches[0].Index;
            time = index + time + word;
            return time;
        }

        public DateTime getTime()
        {
            string timeStr = getTimeStr();
            string year = "";
            string month = "";
            string date = "";
            string hour = "";
            string min = "";
            string sec = "";
            int[] parse = new int[3];
            int k = 0;
            for (int i = 0; i < timeStr.Length; i++)
            {


                if (year.Length == 0)
                {
                    for (int j = 0; j < 4; i++)
                    {

                        int tmp = (int)timeStr[i];

                        if (tmp > 47 && tmp < 58)
                        {
                            year = year + timeStr[i];
                            j++;
                        }
                        else
                        {
                            if (j < 4)
                            {
                                j = 0;
                                year = "";
                            }
                        }
                        if (j > 3)
                        {
                            if ((int)timeStr[i + 1] > 47 && (int)timeStr[i + 1] < 58)
                            {
                                j = 0;
                                year = "";
                            }
                            if ((int)timeStr[i + 1] < 63 && (int)timeStr[i + 1] > 59)
                            {
                                j = 0;
                                year = "";
                            }
                        }

                    }
                }
                if (month.Length < 2)
                {
                    if ((int)timeStr[i] > 47 && (int)timeStr[i] < 58)
                    {
                        month = month + timeStr[i];
                        continue;
                    }
                }
                if (date.Length < 2)
                {
                    if ((int)timeStr[i] > 47 && (int)timeStr[i] < 58)
                    {
                        date = date + timeStr[i];
                        continue;
                    }
                }
                if (hour.Length < 2)
                {
                    if ((int)timeStr[i] > 47 && (int)timeStr[i] < 58)
                    {
                        hour = hour + timeStr[i];
                        continue;
                    }
                }
                if (min.Length < 2)
                {
                    if ((int)timeStr[i] > 47 && (int)timeStr[i] < 58)
                    {
                        min = min + timeStr[i];
                        continue;
                    }
                }

                if (timeStr[i] == ':')
                {
                    parse[k] = i;
                    k++;
                }
                if (k == 2)
                {
                    if (sec.Length < 2)
                    {
                        if ((int)timeStr[i] > 47 && (int)timeStr[i] < 58)
                        {
                            sec = sec + timeStr[i];
                            continue;
                        }
                    }
                }

            }
            if (sec.Length == 0) sec = "0";
            DateTime t = new DateTime(int.Parse(year), int.Parse(month), int.Parse(date), int.Parse(hour), int.Parse(min), int.Parse(sec));
            return t;
        }

        public string toString()
        {
            string str = "";
            str = str + "title: " + Title + "\r\n" + "\r\n";
            str = str + "time: " + Time + "\r\n" + "\r\n";
            str = str + "author: " + Author + "\r\n" + "\r\n";
            str = str + "content: " + Content + "\r\n" + "\r\n";
            str = str + "picUrl: " + string.Join("\r\n", ImageUrl.ToArray()) + "\r\n" + "\r\n";
            return str;
        }
    }
}
