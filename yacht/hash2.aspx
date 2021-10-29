<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hash2.aspx.cs" Inherits="yacht.hash2" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="UTF-8">
	<title>Document</title>
	    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
</head>
<body>
		<!-- Nav tabs -->
<ul class="nav nav-tabs" >
  <li role="presentation" class="active"><a href="#home" role="tab" data-toggle="tab">Home</a></li>
  <li role="presentation" ><a href="#profile" role="tab" data-toggle="tab">Profile</a></li>
  <li role="presentation"><a href="#messages" role="tab" data-toggle="tab">Messages</a></li>
  <li role="presentation"><a href="#settings" role="tab" data-toggle="tab">Settings</a></li>
</ul>

<!-- Tab panes -->
<div class="tab-content">
  <div role="tabpanel" class="tab-pane active" id="home">.1</div>
  <div role="tabpanel" class="tab-pane " id="profile">..2</div>
  <div role="tabpanel" class="tab-pane " id="messages">.3.</div>
  <div role="tabpanel" class="tab-pane" id="settings">.4.</div>
</div>
<script type="text/javascript">
	var hash = location.hash;//獲取到跳轉頁面的引數
    var tab = $('.nav nav-tabs a');
	var con = $('.tab-content .tab-pane');
	console.log(tab);
    console.log(con);
    console.log(hash);
	for(var i=0;i<con.length;i++){
		var mm = con[i];
		var selectCon = "#"+ $(mm).attr('id');
		if(hash == selectCon){
			tab.siblings().removeClass('active');
			con.siblings().removeClass('active');
			$(tab[i]).addClass('active');
			$(con[i]).addClass('active');
		}
	}
	
</script>	
</body>
</html>
