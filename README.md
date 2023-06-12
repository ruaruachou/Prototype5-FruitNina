# Prototype5-FruitNina

创建一个新Vector3时，要用new。无论是单独创建还是创建Vector数组  
  
重点：在Target脚本中获取GameManager的Obj上的GameMnager组件（脚本）的写法  

理解面向对象：在每个不同的Target上加入score参数，然后在AddScore（score）中调用  

粒子使用方式：如果是会消失的物体，可以挂在本体Prefab上  

Button需要有Component.Standalone Input Module才能生效

增加游戏难度的技巧：在难度按钮中添加多次事件，如生成敌人的方法

如何让某些Obj在Time.timeScale=0时，依然运行，即不受暂停影响？
e.g. 如果想让某个粒子FBX执行，只需要在其Update最前面判断是否暂停，如果暂停则直接return，这样就不会执行后面的了。

Button.Onclick.AddListener在Start()中

用协程实现单局倒计时

使光标具有trigger的方案
>1.创建空物体  

>2.获取MousePos鼠标（屏幕坐标）并转为世界坐标  

>3.空物体坐标为MousePos，并为其添加Trigger