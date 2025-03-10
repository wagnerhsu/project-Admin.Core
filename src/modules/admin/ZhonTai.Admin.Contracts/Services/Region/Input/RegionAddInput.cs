﻿using ZhonTai.Admin.Domain.Region;

namespace ZhonTai.Admin.Services.Region;

/// <summary>
/// 添加
/// </summary>
public class RegionAddInput
{
    /// <summary>
    /// 上级Id
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 简称
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    public RegionLevel Level { get; set; }

    /// <summary>
    /// 代码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 驻地
    /// </summary>
    public string Capital { get; set; }

    /// <summary>
    /// 人口（万人）
    /// </summary>
    public int? Population { get; set; }

    /// <summary>
    /// 面积（平方千米）
    /// </summary>
    public int? Area { get; set; }

    /// <summary>
    /// 区号
    /// </summary>
    public string AreaCode { get; set; }

    /// <summary>
    /// 邮编
    /// </summary>
    public string ZipCode { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 热门
    /// </summary>
    public bool Hot { get; set; } = false;

    /// <summary>
    /// 启用
    /// </summary>
    public bool Enabled { get; set; } = true;
}