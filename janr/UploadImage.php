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
	
	if($_FILES['file']['error'] > 0){  
		echo '!problem:';  
		switch($_FILES['file']['error'])  
		{  
		case 1: echo '文件大小超过服务器限制';  
			break;  
		case 2: echo '文件太大！';  
			break;  
		case 3: echo '文件只加载了一部分！';  
			break;  
		case 4: echo '文件加载失败！';  
			break;  
		}  
		exit;  
	} 
	$file=$_FILES['file']['name'];
	if($_FILES['file']['size'] > 1000000){  
		echo '文件过大！';  
		exit;  
	}  	
	$type=pathinfo($file, PATHINFO_EXTENSION);
	if($type!='jpg' && $type!='jpeg' && $type!='png'&& $type!='gif'){  
		echo '文件不是JPG,PNG或者GIF图片！';  
		exit;  
	}
	$md5=md5_file($_FILES['file']['tmp_name']);
	$now = date("YmdHis");  
	$file_path = 'images/'.$now .sprintf("%04d",$user_id). $md5 . '.'.$type;  
	$file_time=date('Y-m-d H:i:s');
	if(is_uploaded_file($_FILES['file']['tmp_name'])){  
		if(!move_uploaded_file($_FILES['file']['tmp_name'], $file_path)){  
			echo 'Move Failed';  
			exit;  
		}  
	}  
	else {  
		echo 'problem!';  
		exit;  
	}
	
	$query="INSERT INTO `janr_image` (`Uploader`,`File`,`Time`) VALUES ('$user_id','$file_path','$file_time');";
	$result=$mysqli->query($query);
	if(!$result){
		echo 'Fail to execute query';
		exit;
	}
	echo "Success"."<File>$file_path</File>";
	$mysqli->close();
?>
