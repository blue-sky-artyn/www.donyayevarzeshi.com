﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Cruder.Core;
using bluesky.artyn;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

using System.Xml.Linq;


public partial class _Default : System.Web.UI.Page
{

    private string giveMeAd(int adStyleId) {
        string dateNow = DateTime.Now.ToString("yyyyMMdd");
        Random ranNumber = new Random();
        int ranNum = 0;string ret = "";

        tblAdCollection AdTbl = new tblAdCollection();
        AdTbl.ReadList(Criteria.NewCriteria(tblAd.Columns.idStyleGrp, CriteriaOperators.Equal, adStyleId));
        
        tblAdPicCollection adPicTbl = new tblAdPicCollection();

        if (AdTbl.Count > 0)
        {
            while (true)
            {
                ranNum = ranNumber.Next(0, AdTbl.Count);
                int dateAd = Convert.ToInt32(AdTbl[ranNum].dateStart.Substring(0, 2) + AdTbl[ranNum].dateStart.Substring(3, 2) + AdTbl[ranNum].dateStart.Substring(6, 4));

                if (dateAd >= Convert.ToInt32(dateNow))
                {
                    adPicTbl.ReadList(Criteria.NewCriteria(tblAdPic.Columns.idAd, CriteriaOperators.Equal, AdTbl[ranNum].id));
                    ret = adPicTbl[0].picAddr + adPicTbl[0].picName;
                    break;
                }
            }
            return (ret);
        }
        else
            return ("none");
            //return ("./img/headadmiddle.jpg");

    }

    private string PopulateRssFeed(string rssLink)
    {
        string RssFeedUrl = rssLink;
        try
        {
            string newsString = "";

            
            XDocument xDoc = new XDocument();
            xDoc = XDocument.Load(RssFeedUrl);
            var items = (from x in xDoc.Descendants("item")
                         select new
                         {
                             title = x.Element("title").Value,
                             link = x.Element("link").Value,
                             pubDate = x.Element("pubDate"),
                             description = x.Element("description").Value
                             // enclosure = x.Element("enclosure").Value
                         });

            if (items != null)
            {
                foreach (var i in items)
                {
                    newsString += "<li class='tweet'><i class='fa fa-bookmark'></i>" +
                                "<div class='tweet-body article-body'>" +
                                "<h3 class='farsi-font-title farsi-dir' style='text-align: center;'>" + i.title + "</h3><p class='farsi-direction farsi-align' style='width: 86%;'>" + i.description + "</p><small class='readme-font readmore'><a href='" + i.link + "'>ادامه مطلب</a></small></div></li>"; 
                }
            }
            return newsString;
        }
        catch (Exception ex)
        {
            string newsString = "error is ( " + ex.ToString() + " )";
            return newsString;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        #region SQL query
        string dateInsert = DateTime.Now.ToString("yyyyMMdd");

        string ConStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblNewsDetails where id IN (select idNews from tblNewsPeriod where DateOfExp > " + dateInsert + ") order by incReview DESC", con);
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    //hotNewsList = dataReader["id"] , ;
                }
            }



        }
        #endregion


        #region Tables recalls


        tblNewsGroupCollection newsGroupSportTbl = new tblNewsGroupCollection();
        newsGroupSportTbl.ReadList();

        tblNewsDetailsCollection newsDetailsSportTbl = new tblNewsDetailsCollection();

        tblNewsMovieCollection newsMovieTbl = new tblNewsMovieCollection();
        newsMovieTbl.ReadList();

        tblNewsPicCollection newsPicTbl = new tblNewsPicCollection();
        newsPicTbl.ReadList();



        tblNewsHitCollection newsHitSportTbl = new tblNewsHitCollection();
        newsHitSportTbl.ReadList();

        tblSliderCollection sliderNewsTbl = new tblSliderCollection();

        tblMovieGalleryCollection videoGalleryTbl = new tblMovieGalleryCollection();
        videoGalleryTbl.ReadList();

        tblMovieGalleryGroupCollection videoGalleryGrpTbl = new tblMovieGalleryGroupCollection();

        tblGalleryPicturesCollection galleryTbl = new tblGalleryPicturesCollection();
        galleryTbl.ReadList();

        #endregion


        #region slider old
        newsDetailsSportTbl.ReadList();

        string sliderString = "<div id='owl-carousel-1' class='news-background-filler owl-carousel owl-theme center-owl-nav'>";
        int counterFiles = 0;
        if (newsDetailsSportTbl.Count > 5)
        {
            for (int i = newsDetailsSportTbl.Count - 1; i > newsDetailsSportTbl.Count - 6; i--)
            {
                sliderNewsTbl.ReadList(Criteria.NewCriteria(tblSlider.Columns.SliderLink, CriteriaOperators.Like, newsDetailsSportTbl[i].id.ToString()));

                if ((newsDetailsSportTbl[i].topPageFileType == 0) && (sliderNewsTbl.Count == 0))
                {
                    sliderString += "<article class='article thumb-article'><div class='article-img'>" +
                                    "<img class='head-news-img' src='./images/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='دنیای ورزشی " + newsDetailsSportTbl[i].topPageFileAddr + "'>" +
                                    "</div><div class='article-body'><ul class='article-info'>" +
                                    "<li class='article-category'><a href='#'>News</a></li><li class='article-type'><i class='fa fa-camera'></i></li></ul>" +
                                    "<p class='sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[i].newsDetSubtitle + "</p>" +
                                    "<h2 class='farsi-position farsi-font farsi-slider-title article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h2>" +
                                    "<ul class='article-meta'>" +
                                    "<li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li>" +
                                    "</ul></div></article>";
                }
                else
                    counterFiles++;
            }
        }
        else
        {
            for (int i = newsDetailsSportTbl.Count - 1; i > 0; i--)
            {
                sliderNewsTbl.ReadList(Criteria.NewCriteria(tblSlider.Columns.SliderLink, CriteriaOperators.Like, newsDetailsSportTbl[i].id.ToString()));

                if ((newsDetailsSportTbl[i].topPageFileType == 0) && (sliderNewsTbl.Count == 0))
                {
                    sliderString += "<article class='article thumb-article'><div class='article-img'>" +
                                    "<img class='head-news-img' src='./images/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='دنیای ورزشی " + newsDetailsSportTbl[i].topPageFileAddr + "'>" +
                                    "</div><div class='article-body'><ul class='article-info'>" +
                                    "<li class='article-category'><a href='#'>News</a></li><li class='article-type'><i class='fa fa-camera'></i></li></ul>" +
                                     "<p class='sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[i].newsDetSubtitle + "</p>" +
                                    "<h2 class='farsi-position farsi-font farsi-slider-title article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h2>" +
                                    "<ul class='article-meta'>" +
                                    "<li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li>" +
                                    "</ul></div></article>";
                }
                else
                    counterFiles++;
            }
        }
        sliderString += "</div></div>";
        //sliderHtml.InnerHtml = sliderString;
        #endregion


        #region New slider

        string sliderStr = ""; string slidertooltipStr = "";
        newsHitSportTbl.ReadList();

        for (int i = 0; i < newsHitSportTbl.Count; i++)
        {
            newsDetailsSportTbl.ReadList(Criteria.NewCriteria(tblNewsDetails.Columns.id, CriteriaOperators.Equal, newsHitSportTbl[i].newsId));

            sliderStr += "<li class='farsi-font'><img src='images/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' alt='" + newsDetailsSportTbl[0].topPageFileAddr + "' title='" + newsDetailsSportTbl[0].newsDetTitle + "' />" +
            "<h2 class='box-show-slider farsi-position farsi-font farsi-slider-title' style='color:white;'><a href='donyaye-varzeshi-news-details.aspx?itemId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].newsDetGist + "</a></h2></li>";

            slidertooltipStr += "<a href='#' title='" + newsDetailsSportTbl[0].newsDetTitle + "'><span><img src='images/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' alt='" + newsDetailsSportTbl[0].topPageFileAddr + "' style='width:85px;'/>" + newsDetailsSportTbl[0].newsDetTitle + "</span></a>";
        }

        sliderItemsHtml.InnerHtml = sliderStr;
        sliderToolTipHtml.InnerHtml = slidertooltipStr;
        #endregion


        #region tabMenu
        string tabsStrings = string.Empty;
        if (newsGroupSportTbl.Count > 0)
        {
            tabsStrings += "<li  class='active'><a data-toggle='tab' href='#tab" + newsGroupSportTbl[0].id + "'>" + newsGroupSportTbl[0].newsGroupTitle + "</a></li>";
        }

        for (int i = 1; i < newsGroupSportTbl.Count; i++)
        {
            tabsStrings += "<li><a data-toggle='tab' href='#tab" + newsGroupSportTbl[i].id + "'>" + newsGroupSportTbl[i].newsGroupTitle + "</a></li>";
        }
        tabsHtml.InnerHtml = tabsStrings;
        #endregion


        #region news in tab
        string newsTabBarString = string.Empty;
        for (int j = 0; j < newsGroupSportTbl.Count; j++)
        {
            newsDetailsSportTbl.ReadList(Criteria.NewCriteria(tblNewsDetails.Columns.idGroup, CriteriaOperators.Equal, newsGroupSportTbl[j].id));
            if (j == 0)
                newsTabBarString += "<div id='tab" + newsGroupSportTbl[j].id + "' class='tab-pane fade in active'><div class='row'>";
            else
                newsTabBarString += "<div id='tab" + newsGroupSportTbl[j].id + "' class='tab-pane fade'><div class='row'>";

            if (newsDetailsSportTbl.Count < 5)
            {
                for (int i = 0; i < newsDetailsSportTbl.Count; i++)
                {
                    newsTabBarString += "<div class='col-md-4 col-sm-6'><article class='article'><div class='article-img'>";

                    if (newsDetailsSportTbl[i].topPageFileType == 0)
                        newsTabBarString += "<a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'><img src='./images/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='" + newsDetailsSportTbl[i].topPageFileAddr + "'></a><ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i>";
                    else if (newsDetailsSportTbl[i].topPageFileType == 1)
                        newsTabBarString += "<a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'><video id='video" + i + "'><source src='./movie/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></a><ul class='article-info'><li class='article-type'><i class='fa fa-video-camera'></i>";
                    else
                        newsTabBarString += "<a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>" + newsDetailsSportTbl[i].topPageFileAddr + "</a><ul class='article-info'><li class='article-type'><i class='fa fa-link'></i>";

                    newsTabBarString += "</li></ul></div><div class='article-body article-body-top'>" +
                                        "<p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[i].newsDetSubtitle + "</p>" +
                                        "<h4 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h4>" +
                                        "<p>" + newsDetailsSportTbl[i].newsDetGist + "</p>" +
                                        "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li>" +
                                        "</ul></div></article></div>";
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    newsTabBarString += "<div class='col-md-3 col-sm-6'><article class='article'><div class='article-img'>" +
                                        "<a href='#'><img src='./images/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='" + newsDetailsSportTbl[i].topPageFileAddr + "'></a>" +
                                        "<ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i></li></ul></div><div class='article-body article-body-top'>" +
                                        "<p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[i].newsDetSubtitle + "</p>" +
                                        "<h4 class='farsi-font farsi-position article-title'><a href='#'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h4>" +
                                        "<p>" + newsDetailsSportTbl[i].newsDetGist + "</p>" +
                                        "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li>" +
                                        "</ul></div></article></div>";
                }

            }
            newsTabBarString += "</div></div>";
        }
        tabNewsDetailsHtml.InnerHtml = newsTabBarString;
        #endregion


        #region Hit news Left
        string hitNewsString = string.Empty;
        hitNewsString = "<div class='farsi-position section-title'><h2 class='farsi-font title'>خبرهای داغ ورزشی</h2></div>";

        if (newsHitSportTbl.Count > 0)
        {
            newsDetailsSportTbl.ReadList(Criteria.NewCriteria(tblNewsDetails.Columns.id, CriteriaOperators.Equal, newsHitSportTbl[newsHitSportTbl.Count - 1].newsId));

            if (newsDetailsSportTbl[0].topPageFileType == 0)
                hitNewsString += "<article class='article col-md-6'><div class='article2-img'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'><img src='images/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' alt='" + newsDetailsSportTbl[0].newsDetTitle + "'></a>" +
                                 "<ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i></li></ul></div>" +
                                 "<div class='article-body article-body-2'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[0].newsDetSubtitle + "</p>" +
                                 "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].newsDetTitle + "</a></h3>" +
                                 "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[0].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[0].incReview + "</li></ul>" +
                                 "<p>" + newsDetailsSportTbl[0].newsDetGist + "</p></div></article>";
            else if (newsDetailsSportTbl[0].topPageFileType == 1)
                hitNewsString += "<article class='article col-md-6'><div class='article2-img'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'><video id='videoTop" + 0 + "'><source src='./movie/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></a>" +
                                 "<ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i></li></ul></div>" +
                                 "<div class='article-body article-body-2'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[0].newsDetSubtitle + "</p>" +
                                 "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].newsDetTitle + "</a></h3>" +
                                 "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[0].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[0].incReview + "</li></ul>" +
                                 "<p>" + newsDetailsSportTbl[0].newsDetGist + "</p></div></article>";
            else
                hitNewsString += "<article class='article col-md-6'><div class='article2-img'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].topPageFileAddr + "</a>" +
                                 "<ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i></li></ul></div>" +
                                 "<div class='article-body article-body-2'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[0].newsDetSubtitle + "</p>" +
                                 "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].newsDetTitle + "</a></h3>" +
                                 "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[0].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[0].incReview + "</li></ul>" +
                                 "<p>" + newsDetailsSportTbl[0].newsDetGist + "</p></div></article>";
            if (newsHitSportTbl.Count > 1)
            {
                newsDetailsSportTbl.ReadList(Criteria.NewCriteria(tblNewsDetails.Columns.id, CriteriaOperators.Equal, newsHitSportTbl[newsHitSportTbl.Count - 2].newsId));
                if (newsDetailsSportTbl[0].topPageFileType == 0)
                    hitNewsString += "<article class='article col-md-6'><div class='article2-img'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'><img src='images/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' alt='" + newsDetailsSportTbl[0].newsDetTitle + "'></a>" +
                                     "<ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i></li></ul></div>" +
                                     "<div class='article-body article-body-2'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[0].newsDetSubtitle + "</p>" +
                                     "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].newsDetTitle + "</a></h3>" +
                                     "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[0].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[0].incReview + "</li></ul>" +
                                     "<p>" + newsDetailsSportTbl[0].newsDetGist + "</p></div></article>";
                else if (newsDetailsSportTbl[0].topPageFileType == 1)
                    hitNewsString += "<article class='article col-md-6'><div class='article2-img'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'><video id='videoTop" + 1 + "'><source src='./movie/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></a>" +
                                     "<ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i></li></ul></div>" +
                                     "<div class='article-body article-body-2'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[0].newsDetSubtitle + "</p>" +
                                     "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].newsDetTitle + "</a></h3>" +
                                     "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[0].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[0].incReview + "</li></ul>" +
                                     "<p>" + newsDetailsSportTbl[0].newsDetGist + "</p></div></article>";
                else
                    hitNewsString += "<article class='article col-md-6'><div class='article2-img'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].topPageFileAddr + "</a>" +
                                     "<ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i></li></ul></div>" +
                                     "<div class='article-body article-body-2'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[0].newsDetSubtitle + "</p>" +
                                     "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].newsDetTitle + "</a></h3>" +
                                     "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[0].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[0].incReview + "</li></ul>" +
                                     "<p>" + newsDetailsSportTbl[0].newsDetGist + "</p></div></article>";
            }
        }


        int lengthTable = 0;
        //switch (newsHitSportTbl.Count)
        //{
        //    case 1:
        //        lengthTable = 1;
        //        break;
        //    case 2:
        //        lengthTable = 0;
        //        break;
        //    case 3:
        //        lengthTable = 1;
        //        break;
        //    case 4:
        //        lengthTable = 2;
        //        break;
        //    default:
        //        lengthTable = newsHitSportTbl.Count - 5;
        //        break;
        //}


        for (int i = newsHitSportTbl.Count - 3; i >= lengthTable; i--)
        {
            newsDetailsSportTbl.ReadList(Criteria.NewCriteria(tblNewsDetails.Columns.id, CriteriaOperators.Equal, newsHitSportTbl[i].newsId));
            hitNewsString += "<article class='article widget-article'><div class='article2-img'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'><img src='images/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' alt='" + newsDetailsSportTbl[0].newsDetTitle + "'></a></div><div class='article-body'>" +
                             "<p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[0].newsDetSubtitle + "</p>" +
                             "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].newsDetTitle + "</a></h3>" +
                             "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[0].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[0].incReview + "</li></ul></div></article>";
        }

        hitNewsSportHtml.InnerHtml = hitNewsString;
        #endregion


        #region news (delete tmep)
        //top and big news
        string newsString = "<div class='col-md-12'><div class='farsi-position section-title'><h2 class='farsi-font title'>اخبار</h2></div></div>";
        if (newsDetailsSportTbl.Count > 2)
        {
            for (int i = newsDetailsSportTbl.Count - 1; i > newsDetailsSportTbl.Count - 3; i--)
            {
                newsString += "<div class='col-md-6 col-sm-6'><article class='article'><div class='article-img'>" +
                              "<a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>";

                if (newsDetailsSportTbl[i].topPageFileType == 0)
                    newsString += "<img src='./images/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='دنیای ورزشی " + newsDetailsSportTbl[i].topPageFileAddr + "'>" +
                                    "</a><ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i></li></ul></div>";
                else if (newsDetailsSportTbl[i].topPageFileType == 1)
                    newsString += "<div><video id='video1'><source src='./movie/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></div>" +
                                  "</a><ul class='article-info'><li class='article-type'><i class='fa fa-video-camera'></i></li></ul></div>";
                else
                    newsString += newsDetailsSportTbl[i].topPageFileAddr +
                                  "</a><ul class='article-info'><li class='article-type'><i class='fa fa-link'></i></li></ul></div>";


                newsString += "<div class='article-body article-body-3'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[i].newsDetSubtitle + "</p>" +
                              "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h3>" +
                              "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li></ul>" +
                              "<p>" + newsDetailsSportTbl[i].newsDetGist + "</p></div>" +
                              "</article></div>";
            }
        }
        else
        {

            if (newsDetailsSportTbl.Count > 0)
            {

                newsString += "<div class='col-md-6 col-sm-6'><article class='article'><div class='article-img'>" +
                             "<a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>";

                if (newsDetailsSportTbl[0].topPageFileType == 0)
                    newsString += "<img src='./images/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' alt='دنیای ورزشی " + newsDetailsSportTbl[0].topPageFileAddr + "'>" +
                                    "</a><ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i></li></ul></div>";
                else if (newsDetailsSportTbl[0].topPageFileType == 1)
                    newsString += "<div><video id='video1'><source src='./movie/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></div>" +
                                  "</a><ul class='article-info'><li class='article-type'><i class='fa fa-video-camera'></i></li></ul></div>";
                else
                    newsString += newsDetailsSportTbl[0].topPageFileAddr +
                                  "</a><ul class='article-info'><li class='article-type'><i class='fa fa-link'></i></li></ul></div>";



                newsString += "<div class='article-body article-body-3'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[0].newsDetSubtitle + "</p>" +
                             "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].newsDetTitle + "</a></h3>" +
                             "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[0].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[0].incReview + "</li></ul>" +
                             "<p>" + newsDetailsSportTbl[0].newsDetDetails + "</p></div>" +
                             "</article></div>";
            }
        }
        //newsHtml.InnerHtml = newsString;
        //small news
        string smallNewsString = string.Empty;
        if (newsDetailsSportTbl.Count > 3)
        {
            for (int i = newsDetailsSportTbl.Count - 1; i > newsDetailsSportTbl.Count - 4; i--)
            {
                smallNewsString += "<div class='col-md-4 col-sm-4'><article class='article'><div class='article-img'>";
                if (newsDetailsSportTbl[i].topPageFileType == 0)
                    smallNewsString += "<img src='./images/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='دنیای ورزشی " + newsDetailsSportTbl[i].topPageFileAddr + "'>";
                else if (newsDetailsSportTbl[0].topPageFileType == 1)
                    smallNewsString += "<div><video id='video1'><source src='./movie/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></div>";
                else
                    smallNewsString += newsDetailsSportTbl[i].topPageFileAddr;
                smallNewsString += "<div class='article-body'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[i].newsDetSubtitle + "</p><ul class='article-info'><li class='article-type'><i class='fa fa-file-text'></i></li></ul></div>" +
                                   "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h3>" +
                                   "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li></ul></div></article></div>";
            }
        }
        else
        {
            for (int i = newsDetailsSportTbl.Count - 1; i >= 0; i--)
            {
                smallNewsString += "<div class='col-md-4 col-sm-4'><article class='article'><div class='article-img'>";
                if (newsDetailsSportTbl[i].topPageFileType == 0)
                    smallNewsString += "<img src='./images/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='دنیای ورزشی " + newsDetailsSportTbl[i].topPageFileAddr + "'>";
                else if (newsDetailsSportTbl[0].topPageFileType == 1)
                    smallNewsString += "<div><video id='video1'><source src='./movie/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></div>";
                else
                    smallNewsString += newsDetailsSportTbl[i].topPageFileAddr;
                smallNewsString += "<div class='article-body'><p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[i].newsDetSubtitle + "</p><ul class='article-info'><li class='article-type'><i class='fa fa-file-text'></i></li></ul></div>" +
                                   "<h3 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h3>" +
                                   "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li></ul></div></article></div>";
            }
        }
        //newsSmallHtml.InnerHtml = smallNewsString;
        #endregion


        #region popular News
        newsDetailsSportTbl.ReadList();
        string popularNews = "<div class='farsi-position section-title'><h2 class='farsi-font title'>اخبار محبوب</h2></div>";

        if (newsDetailsSportTbl.Count > 5)
        {
            for (int i = 0; i < 5; i++)
            {
                popularNews += "<article class='article row-article'><div class='article3-img'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>";

                if (newsDetailsSportTbl[i].topPageFileType == 0)
                    popularNews += "<img src='./images/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='دنیای ورزشی " + newsDetailsSportTbl[i].topPageFileAddr + "'>";
                else if (newsDetailsSportTbl[0].topPageFileType == 1)
                    popularNews += "<div><video id='video1'><source src='./movie/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></div>";
                else
                    popularNews += newsDetailsSportTbl[i].topPageFileAddr;

                popularNews += "</a></div><div class='article-body article-body-4'>" +
                            //"<ul class='article-info'><li class='article-category'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>News</a></li><li class='article-type'><i class='fa fa-file-text'></i></li></ul>" +
                            "<p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[i].newsDetSubtitle + "</p>" +
                            "<h4 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h4>" +
                            "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li></ul>" +
                            "<p>" + newsDetailsSportTbl[i].newsDetGist + "</p></div></article>";
            }
        }
        else
        {
            for (int i = 0; i < newsDetailsSportTbl.Count; i++)
            {
                popularNews += "<article class='article row-article'><div class='article3-img'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>";

                if (newsDetailsSportTbl[i].topPageFileType == 0)
                    popularNews += "<img src='./images/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='دنیای ورزشی " + newsDetailsSportTbl[i].topPageFileAddr + "'>";
                else if (newsDetailsSportTbl[0].topPageFileType == 1)
                    popularNews += "<div><video id='video1'><source src='./movie/news/" + newsDetailsSportTbl[i].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></div>";
                else
                    popularNews += newsDetailsSportTbl[i].topPageFileAddr;

                popularNews += "</a></div><div class='article-body article-body-4'>" +
                            "<p class='sub-title-news sub-title' style='font-size: 9px;'>" + newsDetailsSportTbl[i].newsDetSubtitle + "</p><ul class='article-info'><li class='article-category'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>News</a></li><li class='article-type'><i class='fa fa-file-text'></i></li></ul>" +
                            "<h4 class='farsi-font farsi-position article-title'><a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[i].id + "'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h4>" +
                            "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li></ul>" +
                            "<p>" + newsDetailsSportTbl[i].newsDetGist + "</p></div></article>";
            }
        }
        popularNewsHtml.InnerHtml = popularNews;
        #endregion

        #region Ad
        string ret = "none";
        tblAdStyleCollection adStyleTbl = new tblAdStyleCollection();
        adStyleTbl.ReadList();
        
        for (int i = 0; i < adStyleTbl.Count; i++)
        {
            // ----------------- FOR POSITION OF 980*120 --------------------
            if (adStyleTbl[i].styleWidth > 600 && adStyleTbl[i].styleHeight > 105)
            {
                ret = giveMeAd(adStyleTbl[i].id);
                if (ret != "none")
                    ad1Html.Src = ret;

                ret = giveMeAd(adStyleTbl[i].id);
                if (ret != "none")
                    ad2Html.Src = ret;

                ret = giveMeAd(adStyleTbl[i].id);
                if (ret != "none")
                    ad3Html.Src = ret;

                ret = giveMeAd(adStyleTbl[i].id);
                if (ret != "none")
                    ad4Html.Src = ret;

                ret = giveMeAd(adStyleTbl[i].id);
                if (ret != "none")
                    ad5Html.Src = ret;
            }
            // ----------------- FOR POSITION OF 980*90 --------------------
            else if (adStyleTbl[i].styleWidth > 600 && adStyleTbl[i].styleHeight <= 105)
            {

            }
            // ----------------- FOR POSITION OF 300*250 --------------------
            else if (adStyleTbl[i].styleWidth <= 600 && adStyleTbl[i].styleHeight >= 250)
            {
                ret = giveMeAd(adStyleTbl[i].id);
                if (ret != "none")
                    adSquareMiddleHtml.Src = ret;
            }
        }

        #endregion

        #region More read in side bar
        newsDetailsSportTbl.ReadList(Criteria.NewCriteria(tblNewsDetails.Columns.incReview, CriteriaOperators.IsNotNull));
        string sideNewsSlider = "<div id='owl-carousel-3' class='owl-carousel owl-theme center-owl-nav'>";
        int maxTopLoop = 0;
        if (newsDetailsSportTbl.Count > 6)
            maxTopLoop = newsDetailsSportTbl.Count - 6;

        for (int i = newsDetailsSportTbl.Count - 1; i > maxTopLoop; i--)
        {
            sideNewsSlider += "<article class='article'><div class='article2-img'>" +
                              "<a href='#'><img src='./images//news/" + newsDetailsSportTbl[i].topPageFileAddr + "' alt='Tamasha new " + newsDetailsSportTbl[i].topPageFileAddr + "'></a>" +
                              "<ul class='article-info'><li class='article-type'><i class='fa fa-file-text'></i></li></ul></div><div class='article-body'>" +
                              "<h4 class='farsi-font farsi-position article-title'><a href='#'>" + newsDetailsSportTbl[i].newsDetTitle + "</a></h4>" +
                              "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[i].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[i].incReview + "</li></ul></div></article>";
        }
        sideNewsSlider += "</div>";
        moreReadSideHtml.InnerHtml = sideNewsSlider;
        #endregion


        #region popular News side on the top
        string sideNews = string.Empty;
        int lowRange = 0;

        newsDetailsSportTbl.ReadList();

        if (newsDetailsSportTbl.Count > 26)
            lowRange = newsDetailsSportTbl.Count - 25;

        for (int i = newsDetailsSportTbl.Count - 1; i > lowRange; i--)
            sideNews += "<a class='title-block farsi-font farsi-position article-title' href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[i].newsDetTitle + "</a>";

        popularNewsSideHtml.InnerHtml = sideNews;
        recentNewsSideHtml.InnerHtml = sideNews;

        #endregion


        #region Rss news
        newsString = "";
        string RssFeedUrl;
        tblNewsRssCollection rssNewsTbl = new tblNewsRssCollection();
        rssNewsTbl.ReadList();

        for (int i = 0; i < rssNewsTbl.Count; i++)
        {
            if (rssNewsTbl[i].RssLink.Trim().Length > 3)
            {
                RssFeedUrl = rssNewsTbl[i].RssLink;

                newsString = PopulateRssFeed(RssFeedUrl);
            }
        }

        rssNewsHtml.InnerHtml = newsString;
        #endregion


        #region popular videos
        string popularVideoStr = "<div class='farsi-position section-title'><h2 class='farsi-font title'>ویدیوهای محبوب</h2><div id='nav-carousel-2' class='custom-owl-nav pull-left'></div></div>";
        int MaxLength = 0;
        if (videoGalleryTbl.Count > 7)
            MaxLength = videoGalleryTbl.Count - 7;

        popularVideoStr += "<div id='owl-carousel-2' class='owl-carousel owl-theme'>";
        for (int i = videoGalleryTbl.Count - 1; i >= MaxLength; i--)
        {
            popularVideoStr += "<article class='article thumb-article'><div class='video-pre-small article-video'>" +
                            "<video controls><source src='" + videoGalleryTbl[i].movieAddr + videoGalleryTbl[i].movieName + "' type='video/mp4'>Your browser does not support HTML5 video.</video>" +
                            "</div><div class='article-body'><ul class='article-info'><li class='article-category'><a>Play</a></li>" +
                            "<li class='article-type'><i class='fa fa-video-camera'></i></li></ul>" +
                            "<h4 class='farsi-font farsi-position article-title'><a href='#'>" + videoGalleryTbl[i].movieTitle + "</a></h4>" +
                            "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + videoGalleryTbl[i].insertDate + "</li></ul></div></article>";
        }
        popularVideoStr += "</div>";
        popularVideoHtml.InnerHtml = popularVideoStr;
        #endregion


        #region top news small slider
        string sliderHitNewsStr = "<div class='widget-title'><h2 class='farsi-font title'>اخبار بر جسته</h2></div>";


        sliderHitNewsStr += "<div id='owl-carousel-4' class='owl-carousel owl-theme'>";
        for (int i = 0; i < newsHitSportTbl.Count; i++)
        {
            newsDetailsSportTbl.ReadList(Criteria.NewCriteria(tblNewsDetails.Columns.id, CriteriaOperators.Equal, newsHitSportTbl[i].newsId));

            sliderHitNewsStr += "<article class='article thumb-article'><div class='article3-img'>";

            if (newsDetailsSportTbl[0].topPageFileType == 0)
                sliderHitNewsStr += "<a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'><img src='./images/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' alt='" + newsDetailsSportTbl[0].topPageFileAddr + "'></a>";
            //<ul class='article-info'><li class='article-type'><i class='fa fa-camera'></i>";
            else if (newsDetailsSportTbl[0].topPageFileType == 1)
                sliderHitNewsStr += "<a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'><video id='video" + i + "'><source src='./movie/news/" + newsDetailsSportTbl[0].topPageFileAddr + "' type='video/mp4'>Your browser does not support HTML5 video.</video></a>";
            //<ul class='article-info'><li class='article-type'><i class='fa fa-video-camera'></i>";
            else
                sliderHitNewsStr += "<a href='donyaye-varzeshi-news-details.aspx?newsId=" + newsDetailsSportTbl[0].id + "'>" + newsDetailsSportTbl[0].topPageFileAddr + "</a>";
            //<ul class='article-info'><li class='article-type'><i class='fa fa-link'></i>";

            sliderHitNewsStr += "</div><div class='article-body'>" +
                                //"<ul class='article-info'><li class='article-category'><a href='#'>News</a></li><li class='article-type'><i class='fa fa-video-camera'></i></li></ul>"+
                                "<h3 class='farsi-font farsi-position article-title'><a href='#'>" + newsDetailsSportTbl[0].newsDetTitle + "</a></h3>" +
                                "<ul class='article-meta'><li><i class='fa fa-clock-o'></i>" + newsDetailsSportTbl[0].newsDetInsertDate + "</li><li><i class='fa fa-comments'></i>" + newsDetailsSportTbl[0].incReview + "</li></ul></div></article>";
        }
        sliderHitNewsStr += "</div>";

        hitNewsSliderHtml.InnerHtml = sliderHitNewsStr;
        #endregion


        #region picture galleries
        string pictureGalleriesStr = "";
        int minLengthGallery = 0;

        if (galleryTbl.Count > 12)
            minLengthGallery = galleryTbl.Count - 13;

        for (int i = galleryTbl.Count - 1; i > minLengthGallery; i--)
            pictureGalleriesStr += "<li><a href='picture-gallery.aspx'><img src='" + galleryTbl[i].GalleryPicAddr + galleryTbl[i].GalleryPicName + "' alt=''></a></li>";

        gallery1Html.InnerHtml = pictureGalleriesStr;

        #endregion


    }

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        string dateInsert = DateTime.Now.ToString("yyyyMMdd");
        tblMemberOfDailyEmail dailyMemberTbl = new tblMemberOfDailyEmail();

        #region check existance
        bool flag = true;
        tblMemberOfDailyEmailCollection checkEmailTbl = new tblMemberOfDailyEmailCollection();
        checkEmailTbl.ReadList(Criteria.NewCriteria(tblMemberOfDailyEmail.Columns.memberEmail, CriteriaOperators.Like, txtEmailSub1.Value.Trim()));
        if (checkEmailTbl.Count > 0)
            flag = false;
        #endregion

        if (txtEmailSub1.Value.Trim().Length > 0 && flag)
        {
            dailyMemberTbl.memberName = "";
            dailyMemberTbl.memberSurname = "";
            dailyMemberTbl.memberEmail = txtEmailSub1.Value.Trim();
            dailyMemberTbl.memberInsDate = Convert.ToInt32(dateInsert);
            dailyMemberTbl.memberExpDate = 0;
            dailyMemberTbl.memberRequestToDea = "1";
            dailyMemberTbl.allow = "1";

            dailyMemberTbl.Create();
        }
        else
            if (flag == false)
            txtErrorHtml.InnerText = "ایمیل قبلا ثبت شده است";
        else
            txtErrorHtml.InnerText = "ایمیل را وارد نمایید";
    }
}