﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/menu.master" AutoEventWireup="true" CodeFile="news-details-sport.aspx.cs" Inherits="admin_gallery_normal_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style>
        .btn-in-cp {
            cursor: pointer;
            border: none;
            outline: none;
            display: inline-block;
            font-size: 1em;
            padding: 10px 34px;
            background: #B52E31;
            color: #fff;
            text-transform: uppercase;
            -webkit-transition: all 0.3s ease-in-out;
            -moz-transition: all 0.3s ease-in-out;
            -o-transition: all 0.3s ease-in-out;
            transition: all 0.3s ease-in-out;
        }

            .btn-in-cp:hover {
                background: #252525;
                text-decoration: none;
            }
        /* items to change */
        .change-items {
            margin: 5px 0;
        }

        .left-change-item {
            float: left;
            margin: 0 1%;
            border: 1px solid gray;
            padding: 5px;
            width: 31%;
        }

        .raido-button-list {
            list-style: none;
        }

            .raido-button-list li input {
                float: left;
                margin-right: 5px;
            }

        .textarea-margin-top {
            margin-top: 5px;
        }

        .thumb-image iframe {
            height: 350px;
        }

        .image-extra {
            width: 150px;
            height: 100px;
        }

        .subtitle-news {
            font-style: italic;
            font-size: 14px;
            font-weight: bold;
            text-shadow: grey 1px 1px;
            padding: 0 12px;
            bottom: 15px;
            position: relative;
        }

        .text-line-heigth {
            line-height: 20px;
        }
    </style>
    <!--content-->
    <div class="content">
        <div class="women_main">
            <!-- start content -->
            <div class="row single">
                <div class="det">
                    <div class="single_left">
                        <div class="grid images_3_of_2">
                            <div class="flexslider">
                                <!-- FlexSlider -->
                                <script src="js/imagezoom.js"></script>
                                <script defer="" src="js/jquery.flexslider.js"></script>
                                <link rel="stylesheet" href="css/flexslider.css" type="text/css" media="screen">

                                <script>
                                    // Can also be used with $(document).ready()
                                    $(window).load(function () {
                                        $('.flexslider').flexslider({
                                            animation: "slide",
                                            controlNav: "thumbnails"
                                        });
                                    });
                                </script>
                                <!-- //FlexSlider-->



                                <div class="flex-viewport" style="overflow: hidden; position: relative; max-height: min-content;">
                                    <ul class="slides" style="width: 1200%; transition-duration: 0.6s; transform: translate3d(-864px, 0px, 0px);">
                                        <li class="clone" aria-hidden="true" style="width: 288px; float: left; display: block;">
                                            <div id="setPicHtml" runat="server" class="thumb-image">
                                                <img src="../images/gallery/2.jpg" data-imagezoom="true" class="img-responsive" draggable="false">
                                            </div>
                                        </li>

                                    </ul>
                                </div>
                                <ul class="flex-direction-nav">
                                    <li class="flex-nav-prev"><a class="flex-prev" href="#">Previous</a></li>
                                    <li class="flex-nav-next"><a class="flex-next" href="#">Next</a></li>
                                </ul>
                            </div>
                        </div>
                        <div id="addDetailHtml" runat="server" class="desc1 span_3_of_2">
                            <h3>Mehrab</h3>
                            <br>
                            <span class="code">Group: <a>Kashan</a></span>
                            <p>a short decription of the carpet</p>
                            <div class="price">
                                <span class="text">Price:</span>
                                <span class="price-new">$110.00</span><span class="price-old">$100.00</span>
                                <span class="price-tax">
                                    <label class="checkbox">
                                        <input type="checkbox" name="checkbox"><i></i>include Ex Tax: 13%</label></span><br>
                                <span class="points"><small>Discount 20%</small></span><br>
                            </div>
                            <div class="det_nav1">
                                <h4>Select a size :</h4>
                                <div class=" sky-form col col-4">
                                    <ul>
                                        <li>Size: 12*10</li>
                                        <li>/</li>
                                        <li>Color: Red</li>
                                        <li>/</li>
                                        <li>Shape: Sqaure</li>
                                    </ul>
                                </div>
                            </div>


                        </div>


                        <div class="clearfix"></div>

                    </div>

                    <style>
                        .thumbnail-pictures {
                            width: 150px;
                        }

                        .tumb-ul {
                            float: left;
                            list-style: none;
                            margin: 0 5px;
                        }

                            .tumb-ul iframe {
                                height: 100px;
                                width: 150px;
                            }

                        video {
                            width: 150px;
                        }

                        .subtitle-text {
                            color: #c4c4c4;
                        }
                    </style>

                    <hr />
                    <div id="extraPictureHtml" runat="server" class="single_left">
                        <h3>Pictures: </h3>
                        <ul>
                            <li class="tumb-ul">
                                <img src="../images/news/sport/invoice.jpg" class="thumbnail-pictures" alt="Alternate Text" /></li>
                            <li class="tumb-ul">
                                <img src="../images/news/sport/invoice.jpg" class="thumbnail-pictures" alt="Alternate Text" /></li>
                            <li class="tumb-ul">
                                <img src="../images/news/sport/invoice.jpg" class="thumbnail-pictures" alt="Alternate Text" /></li>
                            <li class="tumb-ul">
                                <img src="../images/news/sport/invoice.jpg" class="thumbnail-pictures" alt="Alternate Text" /></li>
                        </ul>
                    </div>
                    <div class="clearfix"></div>
                    <hr />
                    <div id="extraMovieHtml" runat="server" class="single_left">
                        <h3>Movies: </h3>
                        <ul>
                            <li class="tumb-ul">
                                <img src="../images/news/sport/invoice.jpg" class="thumbnail-pictures" alt="Alternate Text" /></li>
                            <li class="tumb-ul">
                                <img src="../images/news/sport/invoice.jpg" class="thumbnail-pictures" alt="Alternate Text" /></li>
                            <li class="tumb-ul">
                                <img src="../images/news/sport/invoice.jpg" class="thumbnail-pictures" alt="Alternate Text" /></li>
                            <li class="tumb-ul">
                                <img src="../images/news/sport/invoice.jpg" class="thumbnail-pictures" alt="Alternate Text" /></li>
                        </ul>
                    </div>
                    <div class="clearfix"></div>
                    <hr />
                    <div id="extraHypHtml" runat="server" class="single_left">
                        <h3>External Links: </h3>

                    </div>
                    <div class="clearfix"></div>
                    <div class="single_left">
                        <div class="btn_form">
                            <asp:Button ID="btnDel" runat="server" CssClass="btn-in-cp" Text="DEL" OnClick="btnDel_Click" />
                        </div>
                    </div>

                    <div class="grids_of_4">
                        <div class="clearfix"></div>
                        <div class="panel panel-widget forms-panel">
                            <div class="forms">
                                <div class="form-grids widget-shadow" data-example-id="basic-forms">
                                    <div class="form-title">
                                        <h4>Edit news content: </h4>
                                    </div>
                                    <div class="form-body">
                                        <div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">* Goup of information: </label>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlNewsGroup" runat="server" CssClass="farsi-font-text farsi-direction"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">title: </label>
                                                <div class="textarea-margin-top col-sm-9">
                                                    <asp:TextBox ID="txtTitle" class="form-control" runat="server" CssClass="farsi-font-text farsi-direction" placeholder="eg. Give title for educative information" Width="70%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Subtitle: </label>
                                                <div class="textarea-margin-top col-sm-9">
                                                    <asp:TextBox ID="txtSubTitle" class="form-control" runat="server" CssClass="farsi-font-text farsi-direction" placeholder="eg. Give title for educative information" Width="70%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" style="width: 100%; line-height: 34px; float: left; margin-right: 5px;">
                                                <label for="exampleInputPassword1" style="float: left;">Details: </label>
                                                <asp:TextBox ID="txtDetail" class="form-control" runat="server" CssClass="text-line-heigth farsi-font-text farsi-direction" placeholder="A short detail" TextMode="MultiLine" Width="100%" Height="150px"></asp:TextBox>
                                            </div>

                                            <div class="form-group" style="width: 100%; line-height: 34px; float: left; margin-right: 5px;">
                                                <label for="exampleInputPassword1" style="float: left;">Add external links: </label>
                                                <asp:TextBox ID="txtExtLinks" runat="server" CssClass="form-control farsi-font-text farsi-direction" placeholder="Enter external links" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="form-group" style="float: left; width: 100%;">
                                                <fieldset class="div_top">
                                                    <legend>* Add more files</legend>
                                                    <asp:FileUpload ID="fuGallery" runat="server" Style="margin: 3%" />
                                                </fieldset>
                                                <p class="help-block">Please choose one picture of item</p>
                                            </div>
                                            <div class="form-group" style="float: left;">
                                                <asp:Label ID="lblError" runat="server" Text="" Visible="false" ForeColor="#B52E31"></asp:Label>
                                                <asp:Button ID="btnAdd" runat="server" class="btn btn-default" Text="UPDATE ITEM" OnClick="btnUpdate_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="single-bottom2">
                        <!-- reserve in order to have cramped space -->
                    </div>
                </div>
            </div>
            <!-- end content -->
        </div>
    </div>
</asp:Content>

