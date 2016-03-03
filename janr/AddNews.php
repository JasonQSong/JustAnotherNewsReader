<?php
	session_start();
	require('DBConfig.php');
	$mysqli=new mysqli();
	$mysqli->connect($db_host,$db_user,$db_psw,$db_name);
	$mysqli->query("SET NAMES GBK");
	
	if(!isset($_SESSION['user_id'])||$_SESSION['user_id']==''){
		echo 'Log in first';
		exit;
	}
	$user_id=$_SESSION['user_id'];
	
	$news_title=$_POST['news_title'];
	$news_oritime=$_POST['news_oritime'];
	$news_time=date('Y-m-d H:i:s');
	$news_content=$_POST['news_content'];
	$news_tag=$_POST['news_tag'];
	$news_thumb=$_POST['news_thumb'];
	$news_intro=$_POST['news_intro'];
	$news_category=$_POST['news_category'];
	
	$news_content=addslashes($news_content);
	$news_intro=addslashes($news_intro);
	
	$query="INSERT INTO `janr_news` (`Title`,`OriginalTime`,`Time`,`Content`,`Tag`,`Publisher`,`Category`,`Intro`) VALUES ('$news_title','$news_oritime','$news_time','$news_content','$news_tag','$user_id','$news_category','$news_intro');";
	$result=$mysqli->query($query);
	if(!$result){
		echo 'Fail to execute query';
		exit;
	}
	echo 'Success';
	$mysqli->close();
?>