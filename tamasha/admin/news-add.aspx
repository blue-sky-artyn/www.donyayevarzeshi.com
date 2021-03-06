﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/menu.master" AutoEventWireup="true" CodeFile="news-add.aspx.cs" Inherits="admin_need_to_know" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style>
        label {
          margin-right: 15px;
          line-height: 32px;
        }

        .rb-inputs {
          -webkit-appearance: none;
          -moz-appearance: none;
          appearance: none;

          border-radius: 50%;
          width: 16px;
          height: 16px;

          border: 2px solid #999;
          transition: 0.2s all linear;
          margin-right: 5px;

          position: relative;
          top: 4px;
        }

        .rb-inputs:checked {
          border: 6px solid black;
        }
        .comment-text {
            font-size: 9px;
            color: red;
            padding: 5px;
        }
    </style>
    <!--content-->
    <div class="content">
        <div class="women_main">
            <!-- start content -->
            <div class="tab-main">
                <!--/tabs-inner-->
                <div class="tab-inner">
                    <div class="col-md-12 graph-2">
                        <h3 class="inner-tittle">Add news </h3>
                        <div class="form-body">
                            <div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Groups of news: </label>
                                    <asp:DropDownList ID="ddlNewsGroup" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Type of News: </label>
                                    <br />
                                    <input type="radio" name="myradio" ClientIDMode="Static" runat="server" id="rb1" class="rb-inputs" value="0" checked/><label for="rb1">Picture</label><br />
                                    <input type="radio" name="myradio" ClientIDMode="Static" runat="server" id="rb2" class="rb-inputs" value="1"/><label for="rb1">Movie</label><br />
                                    <input type="radio" name="myradio" ClientIDMode="Static" runat="server" id="rb3" class="rb-inputs" value="2" /><label for="rb1">Hyperlink</label><br />
                                    
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Title: </label>
                                    <asp:TextBox ID="txtTitle" runat="server" Width="100%"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Subtitle: </label>
                                    <asp:TextBox ID="txtSubTitle" runat="server" Width="100%"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Description: </label>
                                    <CKEditor:CKEditorControl ID="ckNews" runat="server" Width="100%"></CKEditor:CKEditorControl>
                                </div>

                                <div class="form-group">
                                    <blockquote>
                                    <label for="exampleInputEmail1">Start date: </label>
                                    <asp:TextBox ID="txtStartDate" runat="server" Width="15%" placeholder="eg. 20190101"></asp:TextBox>
                                        <p class="comment-text">* If leave it blank,it starts from today.</p>
                                    <label for="exampleInputEmail1">End date: </label>
                                    <asp:TextBox ID="txtEndDate" runat="server" Width="15%" placeholder="eg. 20190101"></asp:TextBox>
                                        <p class="comment-text">* If leave it blank,there is no expiery day.</p>
                                    </blockquote>
                                </div>

                                <div class="form-group">
                                    <label for="exampleInputEmail1">Hyperlink: </label>
                                    <asp:TextBox ClientIDMode="Static" ID="txtLink" runat="server" Width="100%"></asp:TextBox>
                                </div>
                                <div>
                                    <div class="form-group">
                                        <fieldset class="div_top">
                                            <legend>Select Top File:</legend>
                                            <asp:FileUpload  ClientIDMode="Static" ID="fuGallery" runat="server" Style="margin: 3%" />
                                        </fieldset>
                                    </div>
                                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="#CC3300"></asp:Label>
                                    <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btn btn-default" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="infHtml" runat="server">
                    <div class="col-md-6 graph-2">
                        <h3 class="inner-tittle">Panel with footer </h3>
                        <div class="panel panel-primary two">
                            <div class="panel-heading">Panel with panel-primary class</div>
                            <div class="panel-body ont two">
                                <div>
                                    <img src="../images/sale1.jpg" alt="" style="width: 100%;" />
                                </div>
                                <p>Panel Content Chickweed Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. okra pea winter purslane coriander yarrow sweet pepper radish garlic brussels sprout groundnut summer purslane earthnut pea Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. tomato spring onion azuki bean gourd.</p>
                            </div>
                            <div class="panel-footer">
                                <a href="delete-need.aspx?itemCode=1">Delete</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 graph-2">
                        <h3 class="inner-tittle">Panel with footer </h3>
                        <div class="panel panel-primary two">
                            <div class="panel-heading">Panel with panel-primary class</div>
                            <div class="panel-body ont two">
                                <div>
                                    <img src="../images/sale1.jpg" alt="" style="width: 100%;" />
                                </div>
                                <p>Panel Content Chickweed Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. okra pea winter purslane coriander yarrow sweet pepper radish garlic brussels sprout groundnut summer purslane earthnut pea Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. tomato spring onion azuki bean gourd.</p>
                            </div>
                            <div class="panel-footer">
                                <a href="#">Delete</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 graph-2">
                        <h3 class="inner-tittle">Panel with footer </h3>
                        <div class="panel panel-primary two">
                            <div class="panel-heading">Panel with panel-primary class</div>
                            <div class="panel-body ont two">
                                <div>
                                    <img src="../images/sale1.jpg" alt="" style="width: 100%;" />
                                </div>
                                <p>Panel Content Chickweed Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. okra pea winter purslane coriander yarrow sweet pepper radish garlic brussels sprout groundnut summer purslane earthnut pea Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. tomato spring onion azuki bean gourd.</p>
                            </div>
                            <div class="panel-footer">
                                <a href="#">Panel footer</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 graph-2">
                        <h3 class="inner-tittle">Panel with footer </h3>
                        <div class="panel panel-primary two">
                            <div class="panel-heading">Panel with panel-primary class</div>
                            <div class="panel-body ont two">
                                <div>
                                    <img src="../images/sale1.jpg" alt="" style="width: 100%;" />
                                </div>
                                <p>Panel Content Chickweed Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. okra pea winter purslane coriander yarrow sweet pepper radish garlic brussels sprout groundnut summer purslane earthnut pea Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. tomato spring onion azuki bean gourd.</p>
                            </div>
                            <div class="panel-footer">
                                <a href="#">Panel footer</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
        <!-- end content -->
    </div>

    <script>
        //var radios = document.forms["formA"].elements["myradio"];
        var radios = document.getElementsByClassName("rb-inputs");
        var fileUplouder = document.getElementById("fuGallery");
        var hyperlinkInput = document.getElementById("txtLink");

        fileUplouder.disabled = false;
        hyperlinkInput.disabled = true;

        for(var i = 0, max = radios.length; i < max; i++) {
            radios[i].onclick = function () {
                if (this.value == 0 ) {
                    fileUplouder.disabled = false;
                    hyperlinkInput.disabled = true;
                }
                else if (this.value == 1) {
                    fileUplouder.disabled = false;
                    hyperlinkInput.disabled = true;
                }
                else {
                    fileUplouder.disabled = true;
                    hyperlinkInput.disabled = false;
                }
            }
        }
    </script>

</asp:Content>

