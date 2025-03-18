﻿using FreeSql.DataAnnotations;
using ZhonTai.Admin.Core.Entities;
namespace ZhonTai.Admin.Domain.PrintTemplate;

/// <summary>
/// 打印模板
/// </summary>
[Table(Name = DbConsts.TableNamePrefix + "print_template")]
[Index("idx_{tablename}_01", $"{nameof(Name)}", true)]
public partial class PrintTemplateEntity : EntityVersion
{
    /// <summary>
    /// 名称
    /// </summary>
    [Column(StringLength = 50)]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Column(StringLength = 50)]
    public string Code { get; set; }

    /// <summary>
    /// 模板
    /// </summary>
    [Column(StringLength = -1)]
    public string Template { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    [Column(StringLength = 200)]
    public string Description { get; set; }

    /// <summary>
    /// 启用
    /// </summary>
	public bool Enabled { get; set; } = true;

    /// <summary>
    /// 排序
    /// </summary>
	public int Sort { get; set; }
}