<?php
$db_host="localhost";
$db_user="root";
$db_psw="123456";
$db_name="news";
function fullstep(){
	$query="SELECT (`Password`,`Privilege`) FROM `janr_user` WHERE `UserName`='$user_name';";
	$result=$mysqli->query($query);
	if($result){
	//success
	}
	else{
	//fail
	}
	$result->free();
	$mysqli->close();
}
?>