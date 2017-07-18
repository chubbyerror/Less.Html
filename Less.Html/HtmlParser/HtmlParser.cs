﻿//bibaoke.com

using System;

namespace Less.Html
{
    /// <summary>
    /// Html 解析器
    /// </summary>
    public static class HtmlParser
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Func<SelectorParam, Query> Query(string content)
        {
            return Selector.Bind(HtmlParser.Parse(content));
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="content">要解析的文档</param>
        /// <returns></returns>
        /// <exception cref="ParseException">html 解析错误</exception>
        public static Document Parse(string content)
        {
            //创建标签阅读器
            ReaderBase reader = new TagReader();

            //阅读器上下文
            Context context = new Context(content, HtmlParser.Parse);

            //设置上下文
            reader.Context = context;

            //最大阅读次数
            int maxRead = ushort.MaxValue;

            //读取所有内容
            while (true)
            {
                //执行读取 返回下一个阅读器
                reader = reader.Read();

                //不返回阅读器 读取完毕 跳出
                if (reader.IsNull())
                {
                    break;
                }
                else
                {
                    maxRead--;

                    if (maxRead == 0)
                        throw new ParseException("文档节点过多", content);
                }
            }

            //返回文档元素
            return context.Document;
        }
    }
}
