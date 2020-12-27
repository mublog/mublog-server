using System;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Common.Helpers
{
    public static class TimestampApplier
    {
        public static BaseEntity ApplyTimestamps(this BaseEntity entity)
        {
            var currentTime = DateTime.UtcNow;

            
            if (entity.CreatedDate == default)
            {
                entity.CreatedDate = currentTime;
                entity.UpdatedDate = currentTime;
            }
            else
            {
                entity.UpdatedDate = currentTime;
            }

            return entity;
        }
        
        public static Post ApplyPostTimestamps(this Post post)
        {
            var currentTime = DateTime.UtcNow;
            
            if (post.CreatedDate == default)
            {
                post.CreatedDate = currentTime;
                post.UpdatedDate = currentTime;
                post.PostEditedDate = currentTime;
            }
            else
            {
                post.UpdatedDate = currentTime;
                post.PostEditedDate = currentTime;
            }

            return post;
        }
    }
}