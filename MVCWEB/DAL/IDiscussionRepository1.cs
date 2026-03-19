using MVCWEB.Models.Entities;

namespace MVCWEB.DAL
{
    public interface IDiscussionRepository1
    {
        Task<bool> CreateDiscussion(Discussions ds);
    }
}