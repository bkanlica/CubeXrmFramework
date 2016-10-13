using Cube.XRM.Framework.Core;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.XRM.Framework
{
    public class CubeEntity
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Gets or sets the error detail.
        /// </summary>
        /// <value>The error detail.</value>
        public string ErrorDetail { get; set; }
        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        /// <value>The name of the entity.</value>
        public string EntityName { get; set; }
        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        /// <value>The ID of the entity.</value>
        public Guid ID { get; set; }
        /// <summary>
        /// Gets the CubeBase
        /// </summary>
        /// <value>The Cube.</value>
        public CubeBase cube { get; set; }

        public CubeEntity()
        {
            cube = ObjectCarrier.GetValue<CubeBase>("cube");
        }

        public Result Create()
        {
            try
            {
                Entity entity = new Entity(EntityName);
                entity.Attributes = PrepareEntity();
                Result result = cube.XRMActions.Create(entity);
                if (!result.isError)
                {
                    ID = (Guid)result.BusinessObject;
                    return result;
                }
                else
                    throw new Exception(result.Message);
            }
            catch (Exception ex)
            {
                return new Result(true, ex.Message, null, cube.LogSystem);
            }
        }

        public Result Update()
        {
            try
            {
                Entity entity = new Entity(EntityName);
                entity.Attributes = PrepareEntity();
                Result result = cube.XRMActions.Update(entity);
                if (!result.isError)
                {
                    ID = (Guid)result.BusinessObject;
                    return result;
                }
                else
                    throw new Exception(result.Message);
            }
            catch (Exception ex)
            {
                return new Result(true, ex.Message, null, cube.LogSystem);
            }
        }

        public Result Delete()
        {
            try
            {
                Result result = cube.XRMActions.Delete(ID, EntityName);
                if (!result.isError)
                {
                    ID = (Guid)result.BusinessObject;
                    return result;
                }
                else
                    throw new Exception(result.Message);
            }
            catch (Exception ex)
            {
                return new Result(true, ex.Message, null, cube.LogSystem);
            }
        }

        /// <summary>
        /// Prepares the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Entity.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual AttributeCollection PrepareEntity()
        { throw new NotImplementedException(); }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Entity:{0} ID:{1}", EntityName, ID != null ? ID.ToString() : string.Empty);
        }
    }
}
