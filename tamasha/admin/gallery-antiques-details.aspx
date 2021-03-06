﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/menu.master" AutoEventWireup="true" CodeFile="gallery-antiques-details.aspx.cs" Inherits="admin_gallery_normal_detail" %>

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
                        <div class="btn_form">
                            <asp:Button ID="btnDel" runat="server" CssClass="btn-in-cp" Text="DEL" OnClick="btnDel_Click" />
                        </div>
                    </div>
                    <div class="single-bottom1">
                        <h6>Edit contents</h6>
                       <div class="left-change-item">
                            <div class="change-items">
                                Select group:
                            <asp:DropDownList ID="ddlGroup" runat="server"></asp:DropDownList>
                            </div>
                            <div class="change-items">
                                Item Price:
                        <asp:TextBox ID="txtPrice" runat="server" Width="100px" placeholder="Eg. 1200" Font-Size="14px"></asp:TextBox>
                                $    /
                                <asp:Label ID="lblOldPrice" runat="server" CssClass="price-old" Text="$1200"></asp:Label>
                            </div>
                            <div class="change-items">
                                Item title:
                        <asp:TextBox ID="txtTitle" runat="server" Width="100%" placeholder="Eg. Title of carpet" Font-Size="14px"></asp:TextBox>
                            </div>
                            <div class="change-items">
                                Item Details:
                        <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" Width="100%" placeholder="Eg. Details about carpet" Font-Size="14px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="left-change-item">
                            <fieldset class="div_top">
                                <legend>File input</legend>
                                <asp:FileUpload ID="fuGallery" runat="server" Style="margin: 3%" />
                            </fieldset>
                            <p class="help-block">Please choose one picture of item</p>
                            <hr />
                            <asp:Label ID="lblError" runat="server" Text="" Visible="true" ForeColor="#B52E31"></asp:Label>
                            <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" CssClass="btn-in-cp" OnClick="btnUpdate_Click" />
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

