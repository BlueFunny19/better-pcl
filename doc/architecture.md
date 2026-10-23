# 架构（初版，随调查结果调整）

## 要解决的问题

要在三个不同的宿主上提供同一批功能：

| 宿主 | 运行时 | 加载方式 | 备注 |
|---|---|---|---|
| 官方闭源 PCL 2.99.0.0 | .NET Framework 4.8 | PCL-Harmony | 补丁要对准精确的 IL |
| 开源 Meloong PCL 2.99.0.0 | .NET Framework 4.8 | PCL-Harmony | 云控、遥测已被源码置空；正版登录与 CurseForge 缺凭据 |
| PCL-CE | .NET 10 | 待定（调查中） | 推测 Harmony 现有的 AppDomainManager 方式不适用 |

## 分层

沿用跨加载器 Mod 的常见做法：核心逻辑只写一份，只依赖抽象接口；每个宿主各写一层适配，实现这些接口。这里的"加载器"就是不同版本的 PCL。

```
src/
  BetterPcl.Core/        netstandard2.0   共用逻辑和抽象接口，不引用任何 PCL 类型
  BetterPcl.Host.Pcl/    net48            官方闭源版与开源版的适配（Harmony 插件）
  BetterPcl.Host.Ce/     net10.0          PCL-CE 的适配（等加载方案确定后再建）
test/
  BetterPcl.Core.Tests/  针对 Core 的单元测试
```

- `netstandard2.0` 可以同时被 net48 和 .NET 10 引用，所以 Core 能共用。
- 官方闭源版与开源版先放在同一个适配项目里，用"宿主种类"区分差异；差异大到影响阅读时再拆开。
- 功能按模块放在 Core 里（去云控与遥测、解除下载限制、正版登录、CurseForge），每个模块通过抽象接口声明"需要宿主提供什么"。

## 版本检查

适配层启动时先判断宿主种类与版本；不在支持范围（2.99.0.0 及更新）就记录日志并**不打任何补丁**。注意：Harmony 的调查提示程序集版本号可能不随版本变化，判断依据可能要改用 PCL 自己的 `VersionCode`，需要调查后确定。

## 凭据策略

- 仓库不存放任何密钥或 Client ID 的值。
- CurseForge Key：参照 Fjord 的做法，运行时从官方 CurseForge 程序取得，或由用户自行填写。
- 微软登录的 Client ID：运行时配置（开源版本来就预留了环境变量 `PCL_MS_CLIENT_ID`），具体取得方式后续单独设计。

## 待定事项

- CE 的加载方案（决定是否要改 Harmony 并提 PR）。
- 版本门槛的判断依据。
- Core 里抽象接口的具体切分（等第一个功能落地时再定，避免空想设计）。
