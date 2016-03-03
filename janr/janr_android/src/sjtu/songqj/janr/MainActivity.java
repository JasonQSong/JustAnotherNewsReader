package sjtu.songqj.janr;

import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;

import org.xmlpull.v1.XmlPullParser;

import android.R.bool;
import android.app.Activity;
import android.app.ActionBar;
import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.util.Xml;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;
import android.os.Build;
//import android.provider.DocumentsContract.Document;
import android.content.Intent;
import android.net.Uri;

import java.util.List;  

import javax.xml.parsers.DocumentBuilder;  
import javax.xml.parsers.DocumentBuilderFactory;  

import org.w3c.dom.Element;  
import org.w3c.dom.Node; 
import org.w3c.dom.Document;
import org.w3c.dom.NodeList;  

public class MainActivity extends Activity {
    public final static String EXTRA_MESSAGE = "sjtu.songqj.janr.MESSAGE";
    RefreshableView refreshableView;
    ListView listView;
    ArrayAdapter<SingleNews> adapter;
    static ArrayList<SingleNews> items = new ArrayList<SingleNews>();
    SimpleDateFormat df = new SimpleDateFormat("yyyyMMdd HH:mm:ss");

    Intent i;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        i = new Intent(this,OpenNewsActivity.class);

        if (savedInstanceState == null) {
            /*
             * getFragmentManager().beginTransaction() .add(R.id.container, new
             * PlaceholderFragment()) .commit();
             */

            refreshableView = (RefreshableView) findViewById(R.id.refreshable_view);
            listView = (ListView) findViewById(R.id.list_view);

            adapter = new ArrayAdapter<SingleNews>(this,
                    android.R.layout.simple_list_item_1, items);
            listView.setAdapter(adapter);
            listView.setOnItemClickListener(new OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent, View view,
                        int position, long id) {
                    // open webviewer
                    String s=items.get(position).getSite().toString();
                    
                    i.putExtra(EXTRA_MESSAGE, s);

                    startActivity(i);

                    // TODO Auto-generated method stub
/*
                    Toast.makeText(getBaseContext(), items.get(position),
                            Toast.LENGTH_SHORT).show();
*/
                }
            });
            refreshableView.setOnRefreshListener(
                    new RefreshableView.PullToRefreshListener() {
                        @Override
                        public void onRefresh() throws Exception {
                            items.clear();
                            String path="http://192.168.1.3/janr/GetNews.php?smallerthan=100000&limit=20";
                            URL url=new URL(path);
                            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                            conn.setReadTimeout(5 * 1000);
                            conn.setRequestMethod("GET");
                            
                            InputStream inputStream = conn.getInputStream();
                            
                            //InputStream inputStream = getResources().getAssets().open("news1.xml");
                            DocumentBuilderFactory factory=DocumentBuilderFactory.newInstance();  
                            try {
                                int biggest=0;
                                int limit=30;
                                int count=0;
                                
                                DocumentBuilder builder=factory.newDocumentBuilder();  
                                Document dom=builder.parse(inputStream);  
                                Element root= dom.getDocumentElement();
                                NodeList newses=root.getElementsByTagName("SingleNews");
                                for (int i = 0; i <newses.getLength(); i++) { 
                                    SingleNews news= new SingleNews();
                                    count++;
                                    Element newsNode=(Element) newses.item(i);
                                    //news.setID(newsNode.getAttribute("ID"));
                                    NodeList childNodes=newsNode.getChildNodes();
                                    for (int j = 0; j < childNodes.getLength(); j++) {
                                        Node node=(Node)childNodes.item(j);
                                        if (node.getNodeType()==Node.ELEMENT_NODE){
                                                Element childNode=(Element) node;
                                                
                                                if ("ID".equals(childNode.getNodeName())){
                                                    news.setID(childNode.getFirstChild().getNodeValue());
                                                }
                                               
                                                if ("Title".equals(childNode.getNodeName())){
                                                    news.setTitle(childNode.getFirstChild().getNodeValue());
                                                }
                                                else if ("Author".equals(childNode.getNodeName())){
                                                    news.setAuthor(childNode.getFirstChild().getNodeValue());
                                                }
                                                else if ("Time".equals(childNode.getNodeName())){
                                                    news.setTime(childNode.getFirstChild().getNodeValue());
                                                    
                                                }
                                                else if ("Intro".equals(childNode.getNodeName())){
                                                    news.setIntro(childNode.getFirstChild().getNodeValue());
                                                    
                                                }
                                                else if ("Site".equals(childNode.getNodeName())){
                                                    news.setSite(childNode.getFirstChild().getNodeValue());
                                                    
                                                }
                                    }
                                }
                                    items.add(news);
                                }
                                //SingleNews news = new SingleNews();
                                //news.setTitle("001");
                                //items.add(0,news);
                                /*
                                XmlPullParser parser = Xml.newPullParser();
                                parser.setInput(inputStream,"UTF-8");
                                int eventType = parser.getEventType();
                                
                                SingleNews news= new SingleNews();
                                while (eventType!= XmlPullParser.END_DOCUMENT){
                                    count=0;
                                    
                                    switch(eventType){
                                    case XmlPullParser.START_DOCUMENT: 
                                        break;
                                    case XmlPullParser.START_TAG:
                                       
                                        String tagName=parser.getName();
                                        if (tagName.equals("ID")){
                                            eventType=parser.next();
                                            news.setID(parser.getText());
                                        }
                                        if (tagName.equals("Title")){
                                            eventType=parser.next();
                                            news.setTitle(parser.getText());
                                            //items.add(news);
                                            thisId=news.getID();
                                        }
                                      
                                    case XmlPullParser.END_TAG:
                                        if (parser.getName().equals("SingleNews")){
                                            items.add(news);
                                            news=new SingleNews();
                                        }
                                        break;
                                    case XmlPullParser.END_DOCUMENT:
                                        //smallerthan=false;
                                        break;
                                    }
                                    eventType=parser.next();
                                }
                                */
                                
                                /*
                                String path = "http://10.184.58.75/janr/GetNews.php?largerthan=0&limit=1";
                                URL url = new URL(path);
                                HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                                conn.setReadTimeout(5 * 1000);
                                conn.setRequestMethod("GET");
                                if (conn.getResponseCode() == 200) {
                                    InputStream inStream = conn.getInputStream();
                                        parseXML(inStream);
                                    }
                                    */
                                
                                //getRssNews();
                                //Thread.sleep(1000);
                                //items.add(0, (df.format(new Date())));
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                            refreshableView.finishRefreshing();
                        }
                    }, 0);
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {

        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    /**
     * A placeholder fragment containing a simple view.
     */
    /*
     * public static class PlaceholderFragment extends Fragment {
     * 
     * public PlaceholderFragment() { }
     * 
     * @Override public View onCreateView(LayoutInflater inflater, ViewGroup
     * container, Bundle savedInstanceState) { View rootView =
     * inflater.inflate(R.layout.fragment_main, container, false); return
     * rootView; } }
     */

}
