> 此文件为发布的网站文件，需要发布在IIS服务器上

# 使用步骤

1. 附加数据库文件到SQL Server上
2. 发布网站到IIS上，具体方法请百度
3. 修改Web.config文件中的数据库连接字符串

```xml
<add name="PrivilegeEntities" connectionString="metadata=res://*/PrivilegeModel.csdl|res://*/PrivilegeModel.ssdl|res://*/PrivilegeModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=你的数据库地址(本机：.);initial catalog=PrivilegeMS;user id=你的数据库登录名;password=登录密码;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />

```

4. 重启发布好的网站，即可浏览

