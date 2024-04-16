# LogsByBrady
### Preview
LogsByBrady is a all-in-one logging framework that includes logging functionality for flat files in different formats. Future versions of LogsByBrady will include logging for other logging locations such as SQL Server.

## Dependency Injection (DI)
```cs
builder.Services.AddBradysLogger()
```
By default this will set the default log path as user's desktop and the format as a text file.

### DI Options
```cs
.WithPath("C:/temp");
```
WithPath will set the log path, using a `relative path`

```cs
.UsingJsonFormat();
```
Using JsonFormat will set the log format to `json` format

```cs
.UsingTxtFormat();
```
Using JsonFormat will set the log format to `txt` format

```cs
.UsingAllFormats();
```
Uses all format options for log files.