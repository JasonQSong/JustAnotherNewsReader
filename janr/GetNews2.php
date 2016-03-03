tetettstetset
<?php
	session_start();
	require('DBConfig.php');
	$mysqli=new mysqli();
	$mysqli->connect($db_host,$db_user,$db_psw,$db_name);
	$mysqli->query("SET NAMES utf8");
	
	if (isset($_GET['largerthan']))
		$larger=$_GET['largerthan'];
	if (isset($_GET['smallerthan']))
		$smaller=$_GET['smallerthan'];
	if (isset($_GET['limit']))
		$limit=$_GET['limit'];	
		
	$query="SELECT * FROM `janr_news` WHERE `ID`='0';";
	if(isset($larger)){
		$query="SELECT * FROM `janr_news` WHERE `ID`>'$larger' ORDER BY `ID` ASC LIMIT $limit ;";
	}
	if(isset($smaller)){
		$query="SELECT * FROM `janr_news` WHERE `ID`<'$smaller' ORDER BY `ID` DESC LIMIT $limit ;";
	}
	echo $query;
	$result=$mysqli->query($query);
	if(!$result){
		echo 'Fail to execute query';
		exit;
	}
?>
<?xml version="1.0" encoding="utf-8"?>
<NewsList>
<?php
	while($row =$result->fetch_array() ){
?>
<SingleNews>
<ID><?php echo $row['ID']; ?></ID>
<Title><?php echo $row['Title']; ?></Title>
<Author><?php echo $row['Publisher']; ?></Author>
<Time><?php echo $row['Time']; ?></Time>
<Intro><?php echo $row['Intro']; ?></Intro>
<Site>ViewNews.php?id=<?php echo $row['ID']; ?></Site>
</SingleNews>
<?php
	}
?>
</NewsList>
<?php
	$result->free();
	$mysqli->close();	
?>