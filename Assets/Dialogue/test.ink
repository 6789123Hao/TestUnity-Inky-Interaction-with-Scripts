INCLUDE globals.ink

-> main

=== main ===
#layout:noName
... {random_line}
#speaker:Bob #portait:bob_default 
......
我是突然出现的{games_played}句超过<color=\#F8FF30>三行的句子</color>我也不知道english haoxiang hui biande henqi guaiguaiguaiguai 会变成什么样如果没有空格我还是我吗句子还是句子吗问句还能表达吗伦理还是守恒吗我很在意
#speaker:Katty #portait:katty_happy #bodyup:
啊 你醒了吗?
#speaker:Bob #portait:bob_default 
#layout:default
什么?
#speaker:Katty #portait:katty_happy
啊 没事你先躺好 我去给你倒点水
{random_line == "": -> Choice1 | -> Choosen}



=== main2 ===
#speaker:Bob #portait:bob_default
你觉得眼皮越来越重 闭上了眼睛...
...
......
#panel:off
#panel:on
-
#speaker:Katty #portait:katty_happy
你又睡着了呀?
还好!茶还没凉,先喝一口吧.

#speaker:Bob #portait:bob_default
...
哦..

#speaker:Katty #portait:katty_happy
你想见谁？

~ games_played++

-> END

===Choice1===
#speaker:Bob #portait:bob_default
    + (不是!)
    #speaker: #portait:katty_happy
    你试着摇了摇头，但他似乎没有察觉,离开了房间
        -> chosenString("(不是!)")
    + 别... 走...
    #speaker: #portait:katty_sad
    可声音仿佛不是自己发出的一般微弱. 他并没有听见 ,离开了房间
        -> chosenString("别... 走...")



=== chosenString(string) ===
chosen {string}
~ random_line = string
rl = {random_line}
->main2

=== Choosen ===
我刚才选了啥来着？
{random_line}
~ random_line = ""
->main2

