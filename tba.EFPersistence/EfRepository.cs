using System;
using System.Data.Entity;
using System.Linq;
using tba.Core.Entities;
using tba.Core.Persistence.Interfaces;

namespace tba.EFPersistence
{
    /// <summary>
    ///     Entity Framework based read-write repository.
    /// </summary>
    public sealed class EfRepository<T> : EfReadOnlyRepository<T>, IRepository<T> where T : Entity
    {
        public EfRepository(DbContext context)
            : base(context)
        {
        }

        #region public CRUD Methods

        /// <summary>
        ///     Create an entity in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entity">entity to create</param>
        public void Insert(long userId, T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            // add entity to data store
            Table.Add(entity);

            Audit(userId, entity, Entity.AuditActionType.Insert);

            // update the data store
            Context.SaveChanges();
        }

        /// <summary>
        ///     Update an entity in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entity">entity to update</param>
        public void Update(long userId, T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Audit(userId, entity, Entity.AuditActionType.Update);

            // update to data store
            Table.Attach(entity);
            Context.Entry((Entity)entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        ///     Update entities in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entities">entities to update</param>
        public void Update(long userId, IQueryable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            // process unit of work prior to update in data store
            foreach (T entity in entities)
            {
                Audit(userId, entity, Entity.AuditActionType.Update);
                Table.Attach(entity);
                Context.Entry((Entity)entity).State = EntityState.Modified;
            }

            // update to data store           
            Context.SaveChanges();
        }

        /// <summary>
        ///     Delete an entities that match the domainFilter from the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entities">entities to delete</param>
        public void Delete(long userId, IQueryable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            // loop and delete
            foreach (T entity in entities)
            {
                Audit(userId, entity, Entity.AuditActionType.Delete);

                if (Context.Entry(entity).State == EntityState.Detached)
                    Table.Attach(entity);

                Table.Remove(entity);
            }

            // comit the changes
            Context.SaveChanges();
        }

        /// <summary>
        ///     Delete an entities that match the domainFilter from the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="id">id of the record</param>
        public void Delete(long userId, long id)
        {
            //if (id == null || id == long.Empty)
            //    throw new ArgumentException("id");

            // get our entity
            T entity = Table.Find(id);

            Audit(userId, entity, Entity.AuditActionType.Delete);

            //  delete
            if (Context.Entry(entity).State == EntityState.Detached)
                Table.Attach(entity);
            Table.Remove(entity);

            // comit the changes
            Context.SaveChanges();
        }
        #endregion

        /// <summary>
        /// Provide an audit trail for unsafe actions
        /// </summary>
        /// <param name="userId">the user making the change</param>
        /// <param name="entity">the entity being changed</param>
        /// <param name="auditAuditAction">The change AuditAction (insert,update,delete)</param>
        private void Audit(long userId, T entity, Entity.AuditActionType auditAuditAction)
        {
            // todo (tba 28/2/15):  move DateTime.Now up
           // Context.AuditEntities.Add(Entity.Audit.Create(userId, entity.Id, auditAuditAction, DateTime.Now));
        }
    }
}