<?php
	session_start();
	require('DBConfig.php');
	$mysqli=new mysqli();
	$mysqli->connect($db_host,$db_user,$db_psw,$db_name);
	$mysqli->query("SET NAMES GBK");
	if(isset($_POST['user_name'])){
		$user_name=$_POST['user_name'];
		$user_password=$_POST['user_password'];
		for($i=0;$i<500;$i++)
			$user_password=sha1($user_password);
		if(isset($_POST['new_user'])&&$_POST['new_user']=='1'){
			$query="INSERT INTO `janr_user` (`UserName`,`Password`,`Privilege`) VALUES ('$user_name','$user_password','10');";
			$result=$mysqli->query($query);
			if(!$result){
				echo 'Fail to execute query';
				exit;
			}
			$_SESSION['user_id']=$mysqli->insert_id;
			echo 'Success: Create user';
		}
		else{
			$_SESSION['user_id']='';
			$query="SELECT `ID`,`Password`,`Privilege` FROM `janr_user` WHERE `UserName`='$user_name';";
			$result=$mysqli->query($query);
			if(!$result){
				echo 'Fail to execute query';
				exit;
			}
			while($row =$result->fetch_array() ){
				if(strcmp($row['Password'],$user_password)==0){
					$_SESSION['user_id']=$row['ID'];
					break;
				}
			}
			if($_SESSION['user_id']==''){
				echo 'Username or Password is wrong';
				exit;
			}
			echo 'Success';
		}
		if(isset($_POST['latitude'])&&isset($_POST['longitude'])){
			echo sprintf('<br/><img src="http://st.map.qq.com/api?size=300*300&center=%s,%s&zoom=16"/>',$_POST['longitude'],$_POST['latitude']);
			$query=sprintf("INSERT INTO `janr_location` (`Longitude`,`Latitude`) VALUES ('%s','%s');",$_POST['longitude'],$_POST['latitude']);
			echo $query;
			$result=$mysqli->query($query);
			if(!$result){
				exit;
			}
			$query=sprintf("UPDATE `janr_user` SET `Location`='%s' WHERE `ID`='%s';",$mysqli->insert_id,$_SESSION['user_id']);
			$result=$mysqli->query($query);
			if(!$result){
				exit;
			}
		}
		exit;
	}
?>
<!DOCTYPE HTML>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />  
<script type="text/javascript" src="jquery-2.1.1.min.js"></script>
</head>
<body>
<form action="Login.php" method="post" onsubmit="getLocation()">
用户名：<input type="text" name="user_name" /><br/>
密码：<input type="password" name="user_password" /><br/>
新用户：<input type="checkbox" name="new_user" value="1" /><br/>
<input id="formlatitude" type="hidden" name="latitude" value="31.023021"/>
<input id="formlongitude" type="hidden" name="longitude" value="121.431961"/>
<input type="submit"/>
</form>
<div id="notation"></div>

<script>
var x=document.getElementById("notation");
function getLocation()
  {
  if (navigator.geolocation)
    {
    navigator.geolocation.getCurrentPosition(showPosition);
    }
  else{x.innerHTML="Geolocation is not supported by this browser.";}
  }
function showPosition(position)
  {
  alert(position.coords.latitude);
	var formlatitude=document.getElementById("formlatitude");
	var formlongitude=document.getElementById("formlongitude");
	formlatitude.value=position.coords.latitude;
	formlongitude.value=position.coords.longitude;
  }
</script>

</body>
</html>