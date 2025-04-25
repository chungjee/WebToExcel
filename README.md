
---

# WebToExcel

**WebToExcel** 是一个基于 VSTO 的 Excel 项目，它将 WebView2 嵌入到 Excel 的任务窗格中，提供了一种高效的方式在 Excel 中处理 Json 数据和网络请求。

## 功能简介

1. **嵌入 WebView2：**
   - 在 Excel 的任务窗格中嵌入 WebView2，用户可以浏览网页并与网页进行交互。

2. **Json 数据捕捉与格式化：**
   - 捕捉网络响应中的 Json 数据，并将其自动格式化后插入到 Excel 工作表中。

3. **拦截网络请求并处理 Excel 文件：**
   - 在文件即将下载（如 Excel 文件）时拦截请求，将文件请求到内存中。
   - 对文件内容进行数据清洗后，将清洗后的数据展示到 Excel 工作表中。
##设计原理

## 使用场景

- 高效处理网页中的 Json 数据，快速将其转化为 Excel 表格。
- 拦截网络请求，直接操作即将下载的 Excel 文件，减少手动处理的步骤。
- 在 Excel 中集成浏览器功能，无需切换窗口即可完成复杂的数据处理任务。

## 系统要求

- **操作系统：** Windows 10 或更高版本
- **Microsoft Office：** Office 2016 或更高版本（支持 VSTO 插件）
- **浏览器组件：** Microsoft Edge WebView2 Runtime

## 安装与使用

### 安装步骤

1. 确保系统已安装 [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/).
2. 下载本项目的安装包或克隆代码库：
   ```bash
   git clone https://github.com/chungjee/WebToExcel.git
   ```
3. 根据项目提供的安装说明，完成插件的安装。

### 使用方法

1. 启动 Excel，点击菜单栏中的 **数据聚合** 插件。
2. 在任务窗格中浏览所需网页。
3. 根据需要，捕捉网络响应中的 Json 数据或拦截 Excel 文件的下载请求。
4. 查看处理后的数据自动填充到工作表中。
5. 对于不同网站采集来的数据进行标准化，以便于数据的合并和汇总整理。

## 贡献

欢迎对本项目提出改进建议或提交代码贡献！请参考 [CONTRIBUTING.md](CONTRIBUTING.md) 获取更多信息。

## 许可

本项目采用 MIT 许可证。详细信息请查看 [LICENSE](LICENSE)。

## 联系方式

如有任何问题或建议，请通过 [Issues](https://github.com/chungjee/WebToExcel/issues) 页面提交。

---

如果需要进一步修改或补充内容，请告诉我！
