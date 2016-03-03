<?php
	session_start();
	require('DBConfig.php');
	$mysqli=new mysqli();
	$mysqli->connect($db_host,$db_user,$db_psw,$db_name);
	$mysqli->query("SET NAMES utf8");
	
	if(!isset($_GET['id'])){
		echo 'id not found';
		exit;
	}
	$news_id=$_GET['id'];
	
	$query="SELECT * FROM `janr_news` WHERE `ID`='$news_id';";
	$result=$mysqli->query($query);
	if(!$result){
		echo 'Fail to execute query';
		exit;
	}
	$news_content='';
	while($row =$result->fetch_array() ){
		$news_content=$row['Content'];
	}
	if($news_content==''){
		echo 'No such news.';
		exit;
	}
	$template_filename = "JustAnotherNewsReader/JANR/NewsTemplate.html";
	$handle = fopen($template_filename, "r");
	$template = fread($handle, filesize ($template_filename));
	fclose($handle);
	$page=str_replace("%BODY%",$news_content,$template);
	echo $page;
	$mysqli->close();
?>