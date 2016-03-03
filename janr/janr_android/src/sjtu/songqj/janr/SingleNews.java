package sjtu.songqj.janr;

public class SingleNews {
	private String title;
	private String author;
	private String time;
	private String intro;
	private String site;
	private String id;
	public void news(){
        title="";
        author="";
        time="";
        intro="";
        site="";
        id="";
    }
	public String getID(){
		return id;
	}
	
	public String getTitle(){
		return title;
	}
	public String getAuthor(){
		return author;
	}
	public String getTime(){
	    return time;
	}
	public String getIntro(){
	    return intro;
	}
	public String getSite(){
	    return site;
	}
	public boolean setTitle(String s){
		title=s;
		return true;
	}
	public boolean setAuthor(String s){
		author=s;
		return true;
	}
	public boolean setTime(String s){
        time=s;
        return true;
    }
	public boolean setIntro(String s){
        intro=s;
        return true;
    }
	public boolean setSite(String s){
        site=s;
        return true;
    }
	public boolean setID(String s){
//	    id=Integer.parseInt(s);
	    id=s;
	    return true;
	}
	
	@Override
	public String toString(){
		return title;
	}

}