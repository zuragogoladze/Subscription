using System;
using System.Collections.Generic;

namespace TBC.Capital.Core.ViewModels.API
{
    public class ContactVM
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public LocationVM Location { get; set; }
        public string Address { get; set; }
    }
    public class LocationVM
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
    public class AboutUsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
    public class OurBusinessVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverTitle { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
    public class TeamMemberVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class PaginationTeamMemberVM
    {
        public List<TeamMemberVM> TeamMember { get; set; }
        public PaginationVM Pagination { get; set; }
    }
    public class PaginationResearchVM
    {
        public List<ResearchVM> Researches { get; set; }
        public PaginationVM Pagination { get; set; }
    }
    public class PaginationVacancyVM
    {
        public List<VacancyVM> Vacancies { get; set; }
        public PaginationVM Pagination { get; set; }
    }
    public class PaginationNewsVM
    {
        public List<NewsVM> News { get; set; }
        public PaginationVM Pagination { get; set; }
    }
    public class AboutUsInfoMainPageVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
    }
    public class GalleryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class PaginationVM
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrPage { get; set; }
        public string PrevPageLink { get; set; }
        public string NextPageLink { get; set; }
    }
    public class MainPageSliderVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string YoutubeId { get; set; }
        public string VimeoId { get; set; }
        public bool IsDarkMode { get; set; }
    }
    public class NewsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string ShareImage { get; set; }
        public string Slug { get; set; }
        public DateTime PublishDate { get; set; }
        public List<TagsVM> Tags { get; set; }
        public List<AttachedFilesVM> AttachedFiles { get; set; }
    }
    public class ResearchVM
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string ShareImage { get; set; }
        public string Slug { get; set; }
        public DateTime PublishDate { get; set; }
        public List<AttachedFilesVM> AttachedFiles { get; set; }
    }
    public class AttachedFilesVM
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? Order { get; set; }
        public string File { get; set; }
        public string Name { get; set; }
    }
    public class TagsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CoverVM
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class VacancyVM
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public DateTime CreateDate { get; set; }
        public string Location { get; set; }
        public string Schedule { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ProductVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
    public class PartnerVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
    public class SearchVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public SearchCategory SearchCategory { get; set; }
    }
    public class MenuVM
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int? ParentFooterId { get; set; }
        public int? ParentHeaderId { get; set; }
        public List<MenuVM> Children { get; set; }
    }
    public enum SearchCategory
    {
        News = 0,
        Research = 1,
        OurBusiness = 2
    }
    public class SubscriberVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsSubscribedToResearches { get; set; }
        public int SubscriptionTime { get; set; }
        public List<TagsVM> Tags { get; set; }
    }
}
