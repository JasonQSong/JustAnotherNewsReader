<?php
	session_start();
	require('DBConfig.php');
	$mysqli=new mysqli();
	$mysqli->connect($db_host,$db_user,$db_psw,$db_name);
	$mysqli->query("SET NAMES utf8");
	
	$query="SELECT * FROM `janr_catagory`;";	
	$result=$mysqli->query($query);
	
	$tpl=<<<XML
<xml>
<Catatories>
%s
</Catatories>
</xml>
XML;
	$catagorytpl=<<<XML
<Catagory>
<ID>%s</ID>
<Name>%s</Name>
<Display>%s</Display>
</Catagory>
XML;
	$content="";
	while($row=$result->fetch_array()){
		$catagorycontent=sprintf($catagorytpl,$row['ID'],$row['Name'],$row['Display']);
		$content.=$catagorycontent;
	}
	$response=sprintf($tpl,$content);
	echo $response;
?>