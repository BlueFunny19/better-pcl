# Better PCL

一个基于 [PCL-Harmony](https://github.com/Gu-ZT/PCL-Harmony) 的 PCL 增强 Mod，同时给开源版 PCL 补上它缺少的功能。

> **状态：项目刚初始化，还没有可用功能。**
>
> 本项目是第三方作品，与 Plain Craft Launcher (PCL) 及其开发者没有隶属关系。PCL 由龙腾猫跃（成都瓜皮龙科技有限公司）开发。

## 计划功能

1. 去除云控和遥测。
2. 去除对 MC 下载的限制。
3. 为开源版补全正版（微软）登录。
4. 为开源版补全 CurseForge 搜索与下载。

功能 1、2、4 面向三个版本：官方闭源版、开源 Meloong 版、PCL-CE；功能 3 只面向开源版。

## 支持的版本

主要支持 PCL `2.99.0.0` 及更新版本，其他版本一律标记为不支持（Mod 检测到不支持的版本时不会打任何补丁）。

## 凭据的处理

仓库里**不存放任何密钥、令牌或 Client ID 的具体值**。需要的凭据在运行时由用户本机取得或配置。

## 目录

| 目录 | 说明 |
|---|---|
| `src/` | 源码（尚未创建，结构见 [doc/architecture.md](doc/architecture.md)） |
| `doc/` | 设计与开发约定 |
| `reference/` | 本地只读参考资料（第三方仓库克隆等），不提交 |
| `binary/` | 本地放置三个版本的 PCL 二进制，不提交 |
| `.claude/` | 本地工作辅助目录，不提交 |

## 许可证

[GPL-3.0-only](LICENSE)。

## 致谢

- [PCL / 龙腾猫跃](https://github.com/Meloong-Git/PCL)
- [PCL-Harmony / Gu-ZT](https://github.com/Gu-ZT/PCL-Harmony)
- [Lib.Harmony](https://github.com/pardeike/Harmony)
- 参考了 [Modrinth App](https://github.com/modrinth/code) 与 [Fjord Launcher](https://github.com/hero-persson/FjordLauncherUnlocked) 的做法
