<?php
	session_start();
	require('DBConfig.php');
	$mysqli=new mysqli();
	$mysqli->connect($db_host,$db_user,$db_psw,$db_name);
	$mysqli->query("SET NAMES utf8");
	if(isset($_GET['Delete'])){
		$query=sprintf("DELETE FROM `janr_news` WHERE `ID`='%s';",$_GET['Delete']);
		echo $query;
		$result=$mysqli->query($query);
	}
	$query="SELECT * FROM `janr_news` JOIN `janr_catagory` ON `janr_news`.`Category`=`janr_catagory`.`ID` JOIN `janr_user` ON `janr_news`.`Publisher`=`janr_user`.`ID`;";
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
<th>ID</th>
<th>Title</th>
<th>OriginTime</th>
<th>PublishTime</th>
<th>Publisher</th>
<th>Catagory</th>
<th>Intro</th>
<th>Operation</th>
</tr>
<?php
	while($row =$result->fetch_array() ){
?>
<tr>
<td><?php echo $row[0]; ?></td>
<td><?php echo $row['Title']; ?></td>
<td><?php echo $row['OriginalTime']; ?></td>
<td><?php echo $row['Time']; ?></td>
<td><?php echo $row['UserName']; ?></td>
<td><?php echo $row['Display']; ?></td>
<td><?php echo $row['Intro']; ?></td>
<td><a href ="AdminNews.php?Delete=<?php echo $row[0]; ?>">Delete</a></td>
</tr>
<?php
	}
?>
</table>
</body>
</html>
