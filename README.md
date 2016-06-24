##说明
我是久光，接触Lua到现在近一年左右，在刚开始接触ulua时写了一些demo，给大家参考，
但是中间由于毕业和工作的原因一直没时间维护这些小demo，现在顺利毕业，也稍稍有些空余时间，
后面我会逐步维护几个关于uLua和ToLua的Demo

##demo链接
1.https://github.com/lixiang-share/LuaSpaceShoot.git<br>
这是一个仿造Unity官方案例做的一个太空射击demo，是一个非常入门级的demo，没有任何基础的可以看看<br> 
2.https://github.com/lixiang-share/LuaTetrisDemo.git<br> 
简单粗暴版俄罗斯方块demo，纯lua实现的俄罗斯方块，不涉及复杂的框架，适合稍有基础的同学学习<br> 
3.https://github.com/lixiang-share/Match3Game.git<br> 
简单的三消demo<br> 



Lua版SpaceShoot__Demo简单说明
您好，我是久光，邮箱780632213@qq.com，首先必须说明的一点是本案例参考了Unity官方给的案例做的，
仅仅适用于刚入门的ulua的爱好者，高手请绕道……由于本人是刚刚进入Unity的世界，设计上很不成熟，
希望大家多体谅，如果有问题可以在QQ 群469941220 里提问

一：运行环境：windows7，unity4.6.5，，否则可能会报错，至于4.6.5以上的版本未进行测试如果出错请重新生成Wrap文件，

二：设计思路：这个Demo的总体框架是在俊哥的simpleframework的基础上的，删除了一些较为复杂的逻辑，比如网络、一些封装的管理器组件等，每个lua脚本都对应一个C#类，（这种设计并不合理，下次有机会再修改），我的所有C#脚本都放在了Assets / Scripts / SpaceShoot 目录下，然后在Assets/Scripts / Manager 下的东西也做了自己的修改，当然Demo中也对遵从俊哥的建议使用了pureMVC框架，当然对俊哥原有的逻辑也做了一些删减。对于lua脚本，也有些改变：我的lua脚本都放在了 Assets / Lua / SpaceShoot 中，，logic目录下增加了ObjManager  lua脚本，对GameManager做了轻微的修改。Demo所有需要的资源都放在了Assets / Prefabs目录下

三：改变了一些simpleframeworkd的Editor更能，Game / Generate Assets Bundle 功能：首先需要选中（可以多选）需要生成assetsbundle的预设，然后点击该选项，就会自动将选中的预设打包生成assetsbundle到StreamingAssets目录下。 Game / Copy Lua Files : 会将Assets / Lua 下的所有文件拷贝到StreamingAssets目录下。程序在windows下运行的话会把StreamAssets下的文件解压到 c:/sightp目录，，程序中真正执行的lua脚本也在c盘对应目录下

四：运行说明！！！由于githup上删除了一些工程配信息，，所以需要手动做一些配置：使用Game / Generate Assets Bundle工具重新生成.assetbundle文件，，，，如果c盘存在sight文件夹，则先删除，，，然后生成使用ulua工具生成wrap文件，，，一定要按上述步骤，，否则可能无法运行!!!


最后再次声明:本人ulua初学者，代码也十分简陋，请大家所包含，，，，最后感学@俊哥和其他大神的技术支持！！！
