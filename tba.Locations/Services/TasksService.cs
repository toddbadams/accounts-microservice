using System;
using tba.Core.Persistence.Interfaces;
using tba.Core.Services;
using tba.Core.Utilities;
using tba.Locations.Entities;
using tba.Locations.Models;

namespace tba.Locations.Services
{
    public class LocationsService : EntityService<Location>
    {

        public LocationsService(IRepository<Location> repository)
            : base(repository, typeof(LocationsService).FullName, "location")
        {
        }

        /// <summary>
        /// Fetch an array of locations for a given parent entity
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="parentId">a location parent entity</param>
        /// <returns>array of location read-only models</returns>
        public LocationRm[] Fetch(string tenantId, string userId, string parentId)
        {
            var entities = FetchEntities(tenantId, userId, parentId, true);
            return LocationRm.From(entities);
        }

        /// <summary>
        /// Insert a new location
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="location"></param>
        /// <returns>a read-only location model</returns>
        public LocationRm Insert(string tenantId, string userId, LocationCm location)
        {
            var msg = "Insert" + ". " +
                       string.Format("tenantId={0}, userId={1}, location={2}", tenantId, userId, Serialization.Serialize(location));
            try
            {
                Log.Debug(msg);
                // create an entity
                var e = location.ToEntity(tenantId);
                Repository.Insert(userId, e);
                // return a location read-only view model
                return LocationRm.From(e);
            }
            catch (Exception exception)
            {
                Log.Error(msg, exception);
                throw new ApplicationException("Failed to insert " + location.Description + " " + FriendlyName);
            }
        }

        /// <summary>
        /// Update an existing location
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="id">the id of the location to update</param>
        /// <param name="location">the location update model</param>
        /// <returns>a read-only location model</returns>
        public LocationRm Update(string tenantId, string userId, string id, LocationUm location)
        {
            var msg = "Update" + ". " +
                       string.Format("tenantId={0}, userId={1}, id={2}, location={3}", tenantId, userId, id, Serialization.Serialize(location));
            try
            {
                // multi-tennant security check, location must exists for the client.
                if (!Exists(tenantId, id))
                {
                    var m = "The " + FriendlyName + " submitted for update does not exist.";
                    Log.Error(msg);
                    throw new ApplicationException(m);
                }
                Log.Debug(msg);
                // get the entity by id, we already know it exists for the client.
                var e = Repository.Get(id);
                // update entity
                location.UpdateEntity(e);
                Repository.Update(userId, e);
                // convert it to a read model
                return LocationRm.From(e);
            }
            catch (Exception exception)
            {
                Log.Error(msg, exception);
                throw new ApplicationException("Failed to update the submitted " + FriendlyName);
            }
        }

        /// <summary>
        /// Delete an existing location
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="id">the id of the location to delete</param>
        /// <returns>a read-only location model</returns>
        public LocationRm Delete(string tenantId, string userId, string id)
        {
            var e = (Location)SetActive(tenantId, userId, id, false);
            return LocationRm.From(e);
        }

        /// <summary>
        /// Mark and existing location as open (not complete)
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="id">the id of the location to mark as open</param>
        /// <returns>a read-only location model</returns>
        public LocationRm MarkAsOpen(string tenantId, string userId, string id)
        {
            var msg = "MarkAsOpen" + ". " +
                       string.Format("tenantId={0}, userId={1}, id={2}", tenantId, userId, id);
            try
            {
                // multi-tennant security check, location must exists for the client.
                if (!Exists(tenantId, id))
                {
                    var m = "The " + FriendlyName + " submitted to mark as open does not exist.";
                    Log.Error(msg);
                    throw new ApplicationException(m);
                }
                Log.Debug(msg);
                // get the entity by id, we already know it exists for the client.
                var e = Repository.Get(id);
                // update entity
                LocationUm.MarkEntityAsOpen(e);
                Repository.Update(userId, e);
                // convert it to a read model
                return LocationRm.From(e);
            }
            catch (Exception exception)
            {
                Log.Error(msg, exception);
                throw new ApplicationException("Failed to mark as open the submitted " + FriendlyName);
            }
        }

        /// <summary>
        /// Mark an existing location as complete
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="id">the id of the location to mark as complete</param>
        /// <returns>a read-only location model</returns>
        public LocationRm MarkAsCompleted(string tenantId, string userId, string id)
        {
            var msg = "MarkAsCompleted" + ". " +
                       string.Format("tenantId={0}, userId={1}, id={2}", tenantId, userId, id);
            try
            {
                // multi-tennant security check, location must exists for the client.
                if (!Exists(tenantId, id))
                {
                    var m = "The " + FriendlyName + " submitted to mark as completed does not exist.";
                    Log.Error(msg);
                    throw new ApplicationException(m);
                }
                Log.Debug(msg);
                // get the entity by id, we already know it exists for the client.
                var e = Repository.Get(id);
                // update entity                
                // todo (tba 28/2/15): convert null to SUser
                // todo (tba 28/2/15): get the user with id, validate this user is member of client
                // todo (tba 28/2/15): move DateTime.Now up
                LocationUm.MarkEntityAsCompleted(e, DateTime.Now, null);
                Repository.Update(userId, e);
                // convert it to a read model
                return LocationRm.From(e);
            }
            catch (Exception exception)
            {
                Log.Error(msg, exception);
                throw new ApplicationException("Failed to mark as complete the submitted " + FriendlyName);
            }
        }

    }
}
