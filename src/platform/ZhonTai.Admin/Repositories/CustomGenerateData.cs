﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZhonTai.Admin.Core.Db;
using ZhonTai.Admin.Domain.DictionaryType;
using ZhonTai.Admin.Domain.Dictionary;
using ZhonTai.Admin.Domain.Api;
using ZhonTai.Admin.Domain.Permission;
using ZhonTai.Admin.Domain.User;
using ZhonTai.Admin.Domain.Role;
using ZhonTai.Admin.Domain.UserRole;
using ZhonTai.Admin.Domain.RolePermission;
using ZhonTai.Admin.Domain.Tenant;
using ZhonTai.Admin.Domain.TenantPermission;
using ZhonTai.Admin.Domain.PermissionApi;
using ZhonTai.Admin.Domain.View;
using ZhonTai.Admin.Core.Configs;
using ZhonTai.Common.Extensions;
using ZhonTai.Admin.Domain.Employee;
using ZhonTai.Admin.Domain.Organization;
using ZhonTai.Admin.Core.Db.Data;
using FreeSql;

namespace ZhonTai.Admin.Repositories;

public class CustomGenerateData : GenerateData, IGenerateData
{
    public virtual async Task GenerateDataAsync(IFreeSql db, AppConfig appConfig)
    {
        #region 读取数据

        //admin
        #region 数据字典

        var dictionaryTypes = db.Queryable<DictionaryTypeEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });

        var dictionaries = db.Queryable<DictionaryEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });
        #endregion

        #region 接口

        var apis = db.Queryable<ApiEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });
        var apiTree = apis.Clone().ToTree((r, c) =>
        {
            return c.ParentId == 0;
        },
        (r, c) =>
        {
            return r.Id == c.ParentId;
        },
        (r, datalist) =>
        {
            r.Childs ??= new List<ApiEntity>();
            r.Childs.AddRange(datalist);
        });

        #endregion

        #region 视图

        var views = db.Queryable<ViewEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });
        var viewTree = views.Clone().ToTree((r, c) =>
        {
            return c.ParentId == 0;
        },
       (r, c) =>
       {
           return r.Id == c.ParentId;
       },
       (r, datalist) =>
       {
           r.Childs ??= new List<ViewEntity>();
           r.Childs.AddRange(datalist);
       });

        #endregion

        #region 权限

        var permissions = db.Queryable<PermissionEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });
        var permissionTree = permissions.Clone().ToTree((r, c) =>
        {
            return c.ParentId == 0;
        },
       (r, c) =>
       {
           return r.Id == c.ParentId;
       },
       (r, datalist) =>
       {
           r.Childs ??= new List<PermissionEntity>();
           r.Childs.AddRange(datalist);
       });

        #endregion

        #region 用户

        var users = db.Queryable<UserEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });

        #endregion

        #region 角色

        var roles = db.Queryable<RoleEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });

        #endregion

        #region 用户角色

        var userRoles = await db.Queryable<UserRoleEntity>().ToListAsync(a => new
        {
            a.Id,
            a.UserId,
            a.RoleId
        });

        #endregion

        #region 角色权限

        var rolePermissions = await db.Queryable<RolePermissionEntity>().ToListAsync(a => new
        {
            a.Id,
            a.RoleId,
            a.PermissionId
        });

        #endregion

        #region 租户

        var tenants = db.Queryable<TenantEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });

        #endregion

        #region 租户权限

        var tenantPermissions = await db.Queryable<TenantPermissionEntity>().ToListAsync(a => new
        {
            a.Id,
            a.TenantId,
            a.PermissionId
        });

        #endregion

        #region 权限接口

        var permissionApis = await db.Queryable<PermissionApiEntity>().ToListAsync(a => new
        {
            a.Id,
            a.PermissionId,
            a.ApiId
        });

        //人事
        #region 部门

        var organizations = db.Queryable<OrganizationEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });
        var organizationTree = organizations.Clone().ToTree((r, c) =>
        {
            return c.ParentId == 0;
        },
        (r, c) =>
        {
            return r.Id == c.ParentId;
        },
        (r, datalist) =>
        {
            r.Childs ??= new List<OrganizationEntity>();
            r.Childs.AddRange(datalist);
        });

        #endregion

        #region 员工

        var employees = db.Queryable<EmployeeEntity>().ToListIgnore(a => new
        {
            a.CreatedTime,
            a.CreatedUserId,
            a.CreatedUserName,
            a.ModifiedTime,
            a.ModifiedUserId,
            a.ModifiedUserName
        });

        #endregion

        #endregion

        #endregion

        #region 生成数据

        var isTenant = appConfig.Tenant;

        SaveDataToJsonFile<DictionaryEntity>(dictionaries, isTenant);
        SaveDataToJsonFile<DictionaryTypeEntity>(dictionaryTypes, isTenant);
        SaveDataToJsonFile<UserEntity>(users, isTenant);
        SaveDataToJsonFile<RoleEntity>(roles, isTenant);
        SaveDataToJsonFile<OrganizationEntity>(organizationTree, isTenant);
        SaveDataToJsonFile<EmployeeEntity>(employees, isTenant);
        if (isTenant)
        {
            var tenantIds = tenants?.Select(a => a.Id)?.ToList();
            SaveDataToJsonFile<DictionaryEntity>(dictionaries.Where(a => tenantIds.Contains(a.TenantId.Value)));
            SaveDataToJsonFile<DictionaryTypeEntity>(dictionaryTypes.Where(a => tenantIds.Contains(a.TenantId.Value)));
            SaveDataToJsonFile<UserEntity>(users.Where(a => tenantIds.Contains(a.TenantId.Value)), false);
            SaveDataToJsonFile<RoleEntity>(roles.Where(a => tenantIds.Contains(a.TenantId.Value)));
            organizationTree = organizations.Clone().Where(a => tenantIds.Contains(a.TenantId.Value)).ToList().ToTree((r, c) =>
            {
                return c.ParentId == 0;
            },
            (r, c) =>
            {
                return r.Id == c.ParentId;
            },
            (r, datalist) =>
            {
                r.Childs ??= new List<OrganizationEntity>();
                r.Childs.AddRange(datalist);
            });
            SaveDataToJsonFile<OrganizationEntity>(organizationTree);
            SaveDataToJsonFile<EmployeeEntity>(employees.Where(a => tenantIds.Contains(a.TenantId.Value)));
        }
        SaveDataToJsonFile<UserRoleEntity>(userRoles);
        SaveDataToJsonFile<ApiEntity>(apiTree);
        SaveDataToJsonFile<ViewEntity>(viewTree);
        SaveDataToJsonFile<PermissionEntity>(permissionTree);
        SaveDataToJsonFile<PermissionApiEntity>(permissionApis);
        SaveDataToJsonFile<RolePermissionEntity>(rolePermissions);
        SaveDataToJsonFile<TenantEntity>(tenants);
        SaveDataToJsonFile<TenantPermissionEntity>(tenantPermissions, propsContractResolver: new PropsContractResolver());

        #endregion
    }
}
