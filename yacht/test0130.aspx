<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="test0130.aspx.cs" Inherits="yacht.test0130" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/js/bootstrap.min.js"></script>
    <title></title>
    <script>
        $(document).ready(function () {
           (function () {
            $('.btn-tab-prev').on('click', function (e) {
                e.preventDefault()
                $('#' + $('.nav-item > .active').parent().prev().find('a').attr('id')).tab('show')
            })
            $('.btn-tab-next').on('click', function (e) {
                e.preventDefault()
                $('#' + $('.nav-item > .active').parent().next().find('a').attr('id')).tab('show')
            })
        })();

});
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container p-4">
  <ul class="nav nav-pills nav-fill bg-warning" id="tab-next-prev" role="tablist">
    <li class="nav-item">
      <a class="nav-link active" id="tab1-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="tab1" aria-selected="true">Tab 1</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" id="tab2-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="tab2" aria-selected="false">Tab2</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" id="tab3-tab" data-toggle="tab" href="#tab3" role="tab" aria-controls="tab3" aria-selected="false">Tab 3</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" id="tab4-tab" data-toggle="tab" href="#tab4" role="tab" aria-controls="tab4" aria-selected="false">Tab 4</a>
    </li>
  </ul>
  <div class="tab-content" id="tab-next-prev-content">
    <div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="tab1-tab">
      <p>Content tab 1.</p>
      <a href="#" role="button" class="btn btn-secondary btn-tab-next">Next</a>
    </div>
    <div class="tab-pane fade" id="tab2" role="tabpanel" aria-labelledby="tab2-tab">
      <p>Content tab 2.</p>
      <a href="#" role="button" class="btn btn-secondary btn-tab-prev">Previous</a>
      <a href="#" role="button" class="btn btn-secondary btn-tab-next">Next</a>
    </div>
    <div class="tab-pane fade" id="tab3" role="tabpanel" aria-labelledby="tab3-tab">
      <p>Content tab 3.</p>
      <a href="#" role="button" class="btn btn-secondary btn-tab-prev">Previous</a>
      <a href="#" role="button" class="btn btn-secondary btn-tab-next">Next</a>
    </div>
    <div class="tab-pane fade" id="tab4" role="tabpanel" aria-labelledby="tab4-tab">
      <p>Content tab 4.</p>
      <a href="#" role="button" class="btn btn-secondary btn-tab-prev">Previous</a>
    </div>
  </div>
</div>
    </form>
</body>
</html>
