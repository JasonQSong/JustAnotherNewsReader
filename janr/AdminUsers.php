<?php
	session_start();
	require('DBConfig.php');
	$mysqli=new mysqli();
	$mysqli->connect($db_host,$db_user,$db_psw,$db_name);
	$mysqli->query("SET NAMES utf8");
	
	if(isset($_GET['Delete'])){
		$query=sprintf("DELETE FROM `janr_user` WHERE `ID`='%s';",$_GET['Delete']);
		$result=$mysqli->query($query);
	}
	
	$query="SELECT * FROM `janr_user`;";
	$result=$mysqli->query($query);
	if(!$result){
		echo 'Fail to execute query';
		exit;
	}
	
?>
<!DOCTYPE HTML>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />  
</head>
<body>
<table>
<tr>
<th>UserID</th>
<th>UserName</th>
<th>NickName</th>
<th>Privilege</th>
<th>Operation</th>
<th></th>
</tr>
<?php
	while($row =$result->fetch_array() ){
?>
<tr>
<td><?php echo $row['ID']; ?></td>
<td><?php echo $row['UserName']; ?></td>
<td><?php echo $row['NickName']; ?></td>
<td><?php echo $row['Privilege']; ?></td>
<td><a href ="AdminUsers.php?Delete=<?php echo $row['ID']; ?>">Delete</a></td>
<?php
	if($row['Privilege']=='1'){
?>
<td><a href ="AdminUsers.php?Down=<?php echo $row['ID']; ?>">Remove admin</a></td>
<?php
	}
	else{
?>
<td><a href ="AdminUsers.php?Up=<?php echo $row['ID']; ?>">Set as admin</a></td>
<?php
	}
?>
</tr>
<?php
	}
?>
</table>
</body>
</html>
