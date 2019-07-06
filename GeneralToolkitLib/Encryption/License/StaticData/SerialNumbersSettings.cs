namespace GeneralToolkitLib.Encryption.License.StaticData
{
    public sealed class SerialNumbersSettings
    {
        public enum ProtectedApp
        {
            SecureMemo = 1,
            SearchForDuplicates = 2,
        }

        internal sealed class ProtectedApplications
        {
            internal class PublicKeys
            {
                public const string SecureMemo = @"-----BEGIN RSA PUBLIC KEY-----
                    BgIAAACkAABSU0ExABAAAAEAAQD1d3lsBBFop3+JUclLJKrrxQVnqKfFvtSbDldi
                    i3IU23nonTRMZD2w8QQREiISYKDPOnJ1hrLkQTXZ5kBu/+pQ7uPc25HMWr1lpzNb
                    Y1jjBEQ9mlOjZjnmhAUYAd/Uy8Kb7P9VV1d7Y8MVzLhRZUTboQ3fDgGK5LiomVgI
                    33btBnuPdr868rFifblQPlrvbm1yBl44jL6we72qRh8V4JmyHXNIJF6Jt9DGMq7I
                    z+Wed28OCjBfXRrk1nNd8uE/4AE5NyydS+5ofOeTmOsXAY0jsQLueZzQB3Qe5qnz
                    jR5su6Ijg3x/6wdvJ6G746dn0fFo94gkYQec9669VZJvEhNwBC0y0hHe+plfZrYc
                    Nu0wIy/NXUZfN+NGL9fJ/z2BvWJCoXzx/06UiG0yNRSBj/86RoDegAipj3gRs7ft
                    S2UCl4zLaJSKGoWDHCXePV4yFeMIXub13gr894vnHIAg1MPaw/+ZBGf1ttOprMrm
                    +qK+2wVSAj8JN3L/eOIAAnJ6Zih8NSmVV4dK6lbjfhuj4fOS++DhCoMCDJOnGFdf
                    Tf58l5AchOqiYW9r9g1NeqmrfUCl65HLgJ9zq2jNWg5hr0KUb7A7+ud6J4xVk4C9
                    xJ68hoZNjwahMLqbLV3m6ABCW9IFwtllo/ygrGx3MqkhT7j3PwdDgrkkEKLc9CyI
                    0CYhyQ==
                    -----END RSA PUBLIC KEY-----";

                public const string GeneralToolkitLib = @"-----BEGIN RSA PUBLIC KEY-----
                    BgIAAACkAABSU0ExABAAAAEAAQAxswW8ZrvUsd87+ivYvz7ezboC2cmT7k48hXjz
                    yO9suSBRWHoKKD7gL2KdmfqoBYhC/UcRBFEgVOM1rfx9yinnyEU4ztfCHq9rkoki
                    YK/IZbg6Ci5eZFchhA9NB9QfLQTI6U15eOS0SKNTyrzmaTQ6flh/AFrXA+DF4+35
                    4CQGGbI0b0LDygPoDWAUKLG9IynXAZmp3AwA1YYWBLwdXhkjlnPeUx2dSC3v+Efz
                    q6wWyO0Rqw64CmyDcIhx4fihLcd3CTWeFq1N6aCAP3y1ZCic1Kde2AdGS1DwNtph
                    jREe3O3fOTZABkkuE8Fhfv8LxptEoqU6lia/pzlXwWUN/Y1msJmbCBrxpd3QiOF3
                    M7gQAYiy0nVjao/e/LEajUPu9eNVW7SS+scUAUVBvHUHLYtECxxMMNIOhg9YWfqT
                    raMd+4X2N0WEiijRnRkWLrCbfhsR96na2IfysaUj+UnlEjcIHgszQtSZbVEQlBWy
                    zEvRFJ7p7UQIoScMdV2X6H2xv1mUtGNXn9cp+gEeWUzvBpnc/vsupz6/RSmYkAsx
                    D1eShZvDHtVLlLpe4aQS7VsuRUJhHI/SXAaig0dTGSlXPdCpy5gkAKy3fEGs9xzE
                    qFjJSN9ayG7aKUPBi6kpvzw5ZHO6FJLrxrz1cCu9QdQb9vlPBA5/CgCqTV68YsaM
                    dkDQpw==
                    -----END RSA PUBLIC KEY-----";

                public const string SearchForDuplicates = @"-----BEGIN RSA PUBLIC KEY-----
                    BgIAAACkAABSU0ExABAAAAEAAQCx6Udba0lV1JkHg/VHzLun9wWqxHhHfVsjoJyQ
                    AnDtNVW122gUoHX42puBoSOnsGSIszPQYWmwAu7UM/oeSYFm24nY6BwMP8wbecs2
                    P+vcaCbLA9+RmCCbia8+MncQX1YuAPTlXYbvEn4sTOXgDUTFhUkNQ38HykglUr+v
                    P5sGn7zRENJ3ZPNb8XKXKSnjzIDGK7IJVj25WV1qmwyR/E5zlRYw1cZmS7gs18fw
                    9bw1NdgeixnPIX8RuDkYA0JmBqB9niR82YeYRsL+wO/bQi+1l6RiWVQBWdxX6EWU
                    FOO3idYHQXK8BOnXizyUxnc+cIXR6IOiDP2wDQiOrS6nmxLfisiO80h9Hi779A0h
                    6Ahc5xwWnBe0B65VcZL2eO8H8SHXCTddcRA7M4Dn0Y7jHmyC/iqgLIWLUkDHuw1b
                    8Oe6FHwlw7/jeoPS2ZPGSxPgnMMLhYO6cuzlN2/WddMFj8xYT+uidxjN6afMiVYq
                    5Bb7omDSBMXwqOE/YAw0Aj379UvUahlUJf0GUtjojPZ432qX2LG8QG9Tc8BazoRo
                    jbJ9XyX8nojAI/KCbRnhahIp6L5IepHltPAsgcatmqRyR5aZIXYIV7cHXVXBeOnG
                    +pOhiQvZOnjOZG/fJ/xRG90ZIiUj93THXFDPt3kpSGlQIhDMdtjAMTkPJnit8sIV
                    MFIapQ==
                    -----END RSA PUBLIC KEY-----";
            }

            internal class PrivateKeys
            {
                public const string GeneralToolkitLib = @"-----BEGIN RSA PRIVATE KEY-----
                    BwIAAACkAABSU0EyABAAAAEAAQAxswW8ZrvUsd87+ivYvz7ezboC2cmT7k48hXjz
                    yO9suSBRWHoKKD7gL2KdmfqoBYhC/UcRBFEgVOM1rfx9yinnyEU4ztfCHq9rkoki
                    YK/IZbg6Ci5eZFchhA9NB9QfLQTI6U15eOS0SKNTyrzmaTQ6flh/AFrXA+DF4+35
                    4CQGGbI0b0LDygPoDWAUKLG9IynXAZmp3AwA1YYWBLwdXhkjlnPeUx2dSC3v+Efz
                    q6wWyO0Rqw64CmyDcIhx4fihLcd3CTWeFq1N6aCAP3y1ZCic1Kde2AdGS1DwNtph
                    jREe3O3fOTZABkkuE8Fhfv8LxptEoqU6lia/pzlXwWUN/Y1msJmbCBrxpd3QiOF3
                    M7gQAYiy0nVjao/e/LEajUPu9eNVW7SS+scUAUVBvHUHLYtECxxMMNIOhg9YWfqT
                    raMd+4X2N0WEiijRnRkWLrCbfhsR96na2IfysaUj+UnlEjcIHgszQtSZbVEQlBWy
                    zEvRFJ7p7UQIoScMdV2X6H2xv1mUtGNXn9cp+gEeWUzvBpnc/vsupz6/RSmYkAsx
                    D1eShZvDHtVLlLpe4aQS7VsuRUJhHI/SXAaig0dTGSlXPdCpy5gkAKy3fEGs9xzE
                    qFjJSN9ayG7aKUPBi6kpvzw5ZHO6FJLrxrz1cCu9QdQb9vlPBA5/CgCqTV68YsaM
                    dkDQpxMZbl/pS4eaV9zvVMkrKRKXGqhM95dYpjZXx6Lom+Zqp829CSgMiM8NtuHq
                    0ZTnQZK4Yk3/IYZ/O6aPRq+JXcK+BPRRUCbWeGWP8gdxxda8s8CouPgo4oAHaA1M
                    LMnpoqVwYZpu0a3W17oZki62Li6oGDXzk4S+XNVVCboKghIKP4CEBrBA2DCFWpl1
                    PqEsseqNpUOo81dPrp04SLI0TZf5eAa34bMFc2/5LZt0Mj7ZiuElD3UT9fZIvtM9
                    6d0pJb8BV4dQpRSnvFgEiqwUgqkA5p2dq2bk+IKn3UHLSDHmubqGbgo+3yGnVNQo
                    91qPBbKUtqq3XldjczGiiFl8GN8rL//sQipUwLOyqoNM4/xGjC2HUnTdmIb/AhnN
                    HiRiK3Hag9iog0HlLQCpzdEukmCDulDGEy1uuaQUWa4sLTySrGfYOoIIC+xx2Bbj
                    R7k4sFPLpKUqQZpl1DIeGO7O63+WD9wUOtr5wfpEcQhpHAfXiq3qR+T6zHQOGLE5
                    wNnJgUcB6Jke5SauH+Pw8efTCd2ZomzyiqnjLu08gJtjRFlT/HoVjW84J4PQ9sy1
                    251k6HndR/srzXZU2Bi5UO5nmzZF5IPr3VT9qlKgS6Q1yJ4rym2yoBB2p76pTcV3
                    9m7aOY0OUxfLHuEvy8DvOzf0wedcMWcdQLWdhL28PBhjdJDAZTMjXKD/n9RHG9xx
                    2TR0V36y20lTOwSc6aK0KpfC6QD3j2iwpvlXc8zHi5u5Jd6+k+iXY7USspRrB9JI
                    VdoTFhATteUzTygNBjkdzwCGj+oxXuUCNGD091tr2LPU4wqO3QUo50rWPupFV+AF
                    cLik+DUdOLt0JfKu9HdS80UjF5yMOdG7DkSXsVF5cVPPWGYVRCQI9He5RQPstJsy
                    kmEyGPr7GFFSRKPxbCwucCAl+vECFrNPgLAHx3gNpcmK82usS57kexwbOSiP/8ap
                    pjkzsszmMI8bJgxOTmtkth5weONAEt3e8Cnv1lciTnYhcla2upjY+wyWRMMfGC9o
                    ne0bES8ucBxmmRXdLzPu5W4i0yxeonD0WBRLONys8cJ7iiAy5pbbDv3blg/natAR
                    LIGL4lFNUbWpV5bLTeP4Zy4TDvLa4QoaKXN0BF/hUepzfIicCpeYRL+fmnQrSzQv
                    gAYagXPjxTEhN01lvr7HJv6MpPGkObhJ2BqnoXhPa9t1qdyDanEtNUFOXBcHkXI7
                    pkfaXLp8B2Pu3i1yxIat9Y9uX6jWlMDC2AfmPNZNQykzNkATEIEpm4SuMkXTXvwg
                    Ph3f/htOm/kOCkhczZRgmJ671Ot6rcGbnLWl66cjE8rOHMMWIWurPKT+9w29BWg4
                    XmexAiAZrmfwUK5+kVOtIcFFyhJORigs/kvZLI5/xZ4GrEcMyRCIpbMmHdMJZAtr
                    xp+LKp7sNMUYbR41527rXNq962y/y6oRFscgZHV81AV4Dkp/lwJOYC9ym257VnDk
                    QqMabvpEAv26xRZA9yPRXW8Zf1jLAsGA6Ol15kPbAnHoQCwxEQ8GFjS/eP5USysZ
                    F7wztOV6Bx3lp0RwajBRjJKY9UlbqRcqYiu4kjK6ZcUiLtqz0lrCDFiQ1rTHEO/z
                    kJCMXMzjvubyabQk3Lmh5b/iV4VSNFnxIFsU3Po93x4OEhYIOzAAKjxyJKwQJ15w
                    fJKDuGQRBUZr0ktVQ+/m7BIJTudRvnHcX/n7JDtNaZz1yHLMBe56JvXa+HAM1l2R
                    H8K1PS9qjusmT+7V9DAQY9zkV6Bd4eSEkdDPPgAdmf/jXGFAN7DF053FGY6mTp9i
                    GaLqjlijdzxG5zFOT8J84+F3Do1PtcvwgCArljLtE4kveGbvln/zb1W6JkFfo59J
                    ZkHHSZ2aqBdt4JDxOqY4NR/Z/I8X+U/bJjFOMnSHgAuwCSb/+0h0FFW4dm1TtATy
                    +lAQ10UBeDZCq9ek9f5PQMQVaYViLe4kYG+kQMt4FsHrzi6FijYSwLJVOx72CAH3
                    xq9N7wGJGEb3esk2X7uqlPnuvfUx/ZpHvHYYkd9A8cTR+HDqdMNGHQrHJzPxZ4je
                    YDRvvVWLE+f3uCUZZT4iN3U/91/M1U3+DHVjl0g5apOZfnV2mANK4e5t0oJ0MjbT
                    ix+N33baxYMDk28npGXZhMjm/pk1CPqPWTz5CkIQKlKS7ZqIen1eYSBwPnrV8e41
                    EBs7+uoPnHqOMSr+nHWZdMGApJEywI2Wb1anQuEo1j4kH8+Qi9qeLUpiHQC4lrR/
                    d4sK06wy5U+XGzMO0pWXCKDFB6XnGC3ulntKsjhcV7hngrVhEGrcedb7FDjbZbdq
                    31IxXlxRyJBomJLn56a49PeQHDy3j4vKjTfT8NurQMXAkRwO/vrPp9pG9yIFKEK/
                    E1DJzS3OPITAc5KTqfPrvBYpwl8=
                    -----END RSA PRIVATE KEY-----";

                public const string SecureMemoPrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
                    BwIAAACkAABSU0EyABAAAAEAAQD1d3lsBBFop3+JUclLJKrrxQVnqKfFvtSbDldi
                    i3IU23nonTRMZD2w8QQREiISYKDPOnJ1hrLkQTXZ5kBu/+pQ7uPc25HMWr1lpzNb
                    Y1jjBEQ9mlOjZjnmhAUYAd/Uy8Kb7P9VV1d7Y8MVzLhRZUTboQ3fDgGK5LiomVgI
                    33btBnuPdr868rFifblQPlrvbm1yBl44jL6we72qRh8V4JmyHXNIJF6Jt9DGMq7I
                    z+Wed28OCjBfXRrk1nNd8uE/4AE5NyydS+5ofOeTmOsXAY0jsQLueZzQB3Qe5qnz
                    jR5su6Ijg3x/6wdvJ6G746dn0fFo94gkYQec9669VZJvEhNwBC0y0hHe+plfZrYc
                    Nu0wIy/NXUZfN+NGL9fJ/z2BvWJCoXzx/06UiG0yNRSBj/86RoDegAipj3gRs7ft
                    S2UCl4zLaJSKGoWDHCXePV4yFeMIXub13gr894vnHIAg1MPaw/+ZBGf1ttOprMrm
                    +qK+2wVSAj8JN3L/eOIAAnJ6Zih8NSmVV4dK6lbjfhuj4fOS++DhCoMCDJOnGFdf
                    Tf58l5AchOqiYW9r9g1NeqmrfUCl65HLgJ9zq2jNWg5hr0KUb7A7+ud6J4xVk4C9
                    xJ68hoZNjwahMLqbLV3m6ABCW9IFwtllo/ygrGx3MqkhT7j3PwdDgrkkEKLc9CyI
                    0CYhyePVtExG68viQNs3JyyfHPCndItTUvbwodxQxr64gYboWZC8fSC8bHXOOhXJ
                    RWzOi5f9Xf0sxOVdbETekgpHKKRkWHpLRDhZWLEcrhsnCKSVgxEGIdjbqYJV+8+0
                    EMMI5rPi29MKM+dwYOIvFAO2mmltSkAVSNHsWuHEcan9vBWq/mLsSiZVlFkg7F2+
                    PqSSdgbvfryj5Q+CMxVgVL1qfTRLeDHJ/5F2LrULRtp6hb+kJNHd8bVP6729wQ5K
                    snTGp5NsPgV0X98q79vn4nsATqb5QHCItKMrd4vxVUErr9IZDDoC/XKLqn4ri4iC
                    lCuQxCaHDak9KSYfvfmL3AnzCNpHIo7qd9IJTNa8wQT8Tqaq/xPZp8muvAlErVsH
                    g20u4iosugpss9YItjFnyG0Ir/qsl776ctatcN7Ww1z/twU/zMFxC7mS3y6TPaoE
                    vKhfZKIDWJeubTIHpFgeIliiUtBJ+ct0VVSZqDsD6o1Q87Kacd/CnmLuj+4fS5wi
                    kOxoJFGl/QJ7bXusw+Sc+s5Kx6oUoXPwQ4tqVwVw9ratv66JTE22v3u4sfr0zNww
                    eaYcGU5EHdey/cvDaHhjvNPDXbI5agkM3v8RfgtGyWb4748Z6ufNGQxjlrf7aZYr
                    HwZlCCIbrSEEkEPz+fCyXkc2cIy5hJMoHLxKSeGT2p5doSbsg3/mPyVB5KhdCbgg
                    FxJ8YZ17Bf9Jlo4LLCZ7iHp3syXfAdCpEkx/XDqvvC2QYYXGpp/elog95zLiRA6j
                    lbAYoMlAowQnVQrBQwlXTIkQfcJX2G8spzfNDNo6mDAoe0q8fv9BxFZcIlF+GKyD
                    A6ICi4dadmQYyrPGGIT+JXv3f9gdTcH7QtS+DM4G3LdK/TC11bA9KWvTRl5Q5c9n
                    RiTn6PExD/0lGTmVG0b6OE+oT1qf29tgj2G3PuSJyFsXwbs4W9NR6Qp7axRx/qwW
                    zrseWH+A43NAK94cC13WSGaF37kEwwzrUd8ueicPc1CSGRcGnNmJ+/zB8uYlSflN
                    NhWQbmfeRKpeNmGlqn7L7/Los0piOJwJfHAzqAt1IUtrSTT8QRc3UaDebTPJP47c
                    yr4xCmFZxy/Axil2JmBf5OC4AkLIzxGjsLuWBXyvbXj67arlfiDLtvwpkHum3B9q
                    ECWJAW4hQXE9pn7dIcS4kSDPm1Atkchqe6FhXzbB3euB5Kl324nKTRatvkGY4iK0
                    4K2hyhgeH3AnEykVK2sdC/OyEbAAHB3GrMKYksS3pMpW39aS5Xro6heNxajAoNin
                    ouVPEDHsu0wrgyar+J9ntYEG3LYp7m6OU11Iu01/4dPf3sJutH+hbA8OywH84oi+
                    VECQ7Hs++kLZaqXnZk6EJmHr3t/rTVeMJJnAyKNzUf9V1YGSPlDs/JqCYlVAC1U3
                    +/8jPB0Poxx6PMzvIHuUBK7fduRIYTCZqNO6TW+0HLbY7o/X2sSQoSR+jrvSYNdn
                    tD9GHyks/y+2VlJDXiEfw+O1DswePjvAZQUmefCdDUVFZixhfRKSTE5/bad8yjUp
                    Z9sjg+zZghghHNbIVhAnHDCrAF0i964rL7EgdAQkZrFbwN14IMIW6b0jffxQrJmM
                    ZmXNoss+IQGm7/nsSp4kHhfisGMEB/wKQLmS91HH0X48ad1QEZns3k0WVyyUEQ5J
                    nG3EnuqfllaTj/Fx8Xf07zOJnTk95zGIZeFQw0q+oJICEvK3wfZeRTpExNhd3OAK
                    INYt5TIaQ5ip/e23mlDBotJa6Gdvp0smv7gjeOXn6A7WVJ7AkY7T/eqQVqcb5H1L
                    UTUOUDcG9m/VE4mNZOS2n2Eh8Q6dFVZmZ1Vozg1LcsZx7MczBDkNs/Lq0OeNKgO/
                    XrUOzEkAGZtRXeMcRNOhcT9sWzONahbdhUn1hmusVmoEadeV2kefgyw6M0oeBa7H
                    BlFc4zkglwCM5JsuCd8T+mYLchwDclfO9bUxuQKZF/GNM1XLxtdfo07NuVy0zNaH
                    3sEj/JPRhEsAvgHboiUYhWr6GS/TtcRVO/X6V7LGBScdMFgdAjBUx2t6BqAZ78Nn
                    R7jif345lqd1+Xvbv0pypV0HSziVYHB+3ypfUgXsS7WLb735pNgG+rpzuvewFepi
                    xf/XHqa7DKdduTH6/2icSlBBdw2f3FWVOXWwzGJrOTk2d2J/NcCyqS1rQHhCGUZe
                    hufx+4y4CVScZ7COF3A3QhOk/AHJBQTGyJfYfiFU3u6QT7sGQ/kQNexDnuD2PQuO
                    5DaqmTtHGUFFH95ry/nz6xfp9Fd0wDVKKHNe85tx3BYaSr0hCpjwJIUy0dstoTBH
                    lyijZbbYvQDeeBBhaIdcpyygHOYVNFVRPpHlZHv3SiKzIbAYou1piQN3BypmkS5L
                    h1gzE4W/SbznLsVvBumQiXoQFR8=
                    -----END RSA PRIVATE KEY-----";

                public const string SearchForDuplicates = @"-----BEGIN RSA PRIVATE KEY-----
                    BwIAAACkAABSU0EyABAAAAEAAQCx6Udba0lV1JkHg/VHzLun9wWqxHhHfVsjoJyQ
                    AnDtNVW122gUoHX42puBoSOnsGSIszPQYWmwAu7UM/oeSYFm24nY6BwMP8wbecs2
                    P+vcaCbLA9+RmCCbia8+MncQX1YuAPTlXYbvEn4sTOXgDUTFhUkNQ38HykglUr+v
                    P5sGn7zRENJ3ZPNb8XKXKSnjzIDGK7IJVj25WV1qmwyR/E5zlRYw1cZmS7gs18fw
                    9bw1NdgeixnPIX8RuDkYA0JmBqB9niR82YeYRsL+wO/bQi+1l6RiWVQBWdxX6EWU
                    FOO3idYHQXK8BOnXizyUxnc+cIXR6IOiDP2wDQiOrS6nmxLfisiO80h9Hi779A0h
                    6Ahc5xwWnBe0B65VcZL2eO8H8SHXCTddcRA7M4Dn0Y7jHmyC/iqgLIWLUkDHuw1b
                    8Oe6FHwlw7/jeoPS2ZPGSxPgnMMLhYO6cuzlN2/WddMFj8xYT+uidxjN6afMiVYq
                    5Bb7omDSBMXwqOE/YAw0Aj379UvUahlUJf0GUtjojPZ432qX2LG8QG9Tc8BazoRo
                    jbJ9XyX8nojAI/KCbRnhahIp6L5IepHltPAsgcatmqRyR5aZIXYIV7cHXVXBeOnG
                    +pOhiQvZOnjOZG/fJ/xRG90ZIiUj93THXFDPt3kpSGlQIhDMdtjAMTkPJnit8sIV
                    MFIapQdJXqePtoYHcJVQBaXukO3Y0GjQhaPTtz8zM59XzLEAl8tVZxwI95UyOL1z
                    /wkjYmj7O+APF3lrzF0cwteq3zTGf/GuwTaJtBA02+euDDZvPjj6OTxr2Uu7VJVa
                    9QutI8Wsr2iMM5RMF80vzwzR8VXRUl4bO+gLgqHjq3CJ4TqjNhgn1cUSarPj14Ly
                    my49yTF79gq2QRdDSUD8sorosdCcXILqpCB0sT/IJBaaCmjlf1nyFHp4/kj7li6r
                    d0Sp820w3ytX8IU2Veo23/vaJP1XCKJqI4oDncGBEQFLaVjZAypXuPOSHplQFjTH
                    HgI9F8p4ai5QlqzHqtNjFqA34NiHoc5vaZh4umJRQzs7uDyerN64oTLgQdGEy3AA
                    cU0+DfrYtn7oCSFjPh2alKa9nxUHqgAMRqHq87ixGmEkPoBqIpGJfQgQRbTU05Vt
                    nTwBs9JTMu4uNO68OBbDFX4Ujh6ooh9ByJ9ajyqU4Dbk6RPdG1TSINo6lXeLKy8q
                    d5HM/miyBfDnBjqley62JAImDyyL2u0dNy9vvJDCw/AreeyrIiY1Treoz+tMtBZm
                    5RPpkq+cssYByzZcc9DXreN/iTE3DWn03xnuPhFXxGsCKHDlN+K8KVzc5eYxr+Ei
                    8eHD6FCh3e0AgBQeSPBOLvvRci2IJdJwR/PEH/Tl2f/DHePCTdP9bm4bI2o76HtV
                    Dz5HydUycPEdpIdpSCAnnt5wj1pAF2l4vlM7jGLRfdYdlxSBX0g+QbGj9jeWHJvc
                    5riudrZYUo/FgCWSxYeSa0vbopdIbHqP0N0JEGvS+aRwLQQM8bWsWFct8hpD4fLB
                    5Bl5d8a9qvNlAthyOdrogEl2j2tFp9XsrmKZEsb0lzNHQJnbkBKKsCKWI4pIcR3Y
                    XzikR21q8lMHUDW481YD7xphcrwjM9ZfKK8Gpdg3+8AJoyN09i9CtJLOSqSMJDDQ
                    WIJLv06MzFDxXYzRdGdGQrcw1ut59/8ZGLWqrryTdSByG/S7Vrtqmhdae1FcoI6r
                    CcBRnm14Qw9fZnApabgY2vh+gnzExmGBZvtasPddKrFqWANvrnmONyQuwoxpYYHt
                    WqNF/XAqaOI+tVXOaqCm/Q+tQKM9wQBLlHjBpi7Mou/2NnJB9ZGczqhvS9JdXMBA
                    31d77dO3aC/lvBj8aaZj87Ld1VxHNKFr5nReyxIURIm2Mtg+quejvbTFhF38TELb
                    SnsTUuRDMzR4u4ILL5WAI9uGZn9t86YNBrx41Sq7a3/poycSUHjn92xbeCTHVqFn
                    3coE5L6tPiHnvN+gYVl2syzdIpmLqvxsdLCj7RJjsyaWHtoTH+QOKeAgUVoY8BLV
                    d2/Q6t+Cjq4AHGXJorGYdiP/kjyfCcqVHG86VB6zAjqF6UWkRRFv6e03H0urQnau
                    qWilHnIi3SYHK2hW6zZmW2JBT4znzHZ/RbvcrrM9ASnGmnMfPbaophQnR0Nvf2dI
                    E2joIbN5LkEwahRwl9WMlyuVYJTqzDUQohdQrJ7502mK11DcAMkz6M3YpuWyuN+H
                    zcv+yQapRJ/Qoe+9k92Gl+taCXRk6Y02uTIG/ZG+KQRsNdA9oV49XMR/2s9E0+f7
                    Cx52BnxWDNP27NYOarx0JDGrAbtDuif5r6Vn3FQfoIKljOwKx+fx5c2MjuuMpv5F
                    USsAhW5DATEVqnpfbEVfnI7fNcB8R49xp7becbAl6XMTxN5uLVBtncpiU9ngT1aC
                    loWb7lNB1jXzU1s4ZH3A3TqYC/gdX0OI3MP2hpQlYYPZVzSAjcxii690WUfZd6Ny
                    SyQapfKEz1mkneDzr6180z6Z4u4Z1bPR/r+MsE3wdHhBvHq0KLukW3ekOfJm4OWM
                    Umvu82wcP1gR1S/WtZAxkTlf7EQkTrQFLjIaA/lezn15ZcJlBdseqp+x8RydCIDV
                    oBhJaPAce9zyspyOrPcP5rOO1F07/k4FIWIz2CNNKalAu/+z9e1UOudNu0lHSfv3
                    2sCAYxe7g+Gl9EtefrZ3TQFAAur1PhNy2QGRr2JyubudHLJpFUFXuu3b5AORLmGY
                    CfIn/+zPeIzVPEp2Xf54L5kMxhbAZUwm4uO8Nic6HF2/TW7zDSLKGeh3sggSxKMX
                    kWXYJYsSKVijnrOtCIYkAucUn7PNuuWPgm2SJsfaMW0GoGvmbjjSnFUE1JbEfDP4
                    xMcJfuX749vvE7VXlMcAvqezBVZLxn/TPhYnQ6oN31lFwG9XZoEPL0PujbeO3Z5m
                    80T9VMqJR6BMYvYExvrirZh9j/QMACWtfS0CZ/d60u1J3wWmu1qXppiIjsCM/BFh
                    TE0c5iv0GwXz84v//oJ6yff6vzy9adWjyb9dRzHvq3wHLhr9Z/XlUZBoQEfb0PFF
                    Ex8aF7M6C7I+8hJV5orqs0wPG5U=
                    -----END RSA PRIVATE KEY-----";
            }

            internal class SaltData
            {
                public const string GeneralToolkit = "AuwfOvBpiAnnqqH9ee9yNsQ0ig9oZSczbg9sUfSAkAqE27feiJuLac5r04eBBiGGFpYAKbYA44UGuAXDuIXMygjcecoTR8rz71Xn2ZXvfuf9RSJnC9HYRBbPOgPy11NATANZbXTNuW5LYeeoXBwOi0LQjsUA7Oku1Zp3crraIwmOvrJnI1biPkQe31Pct9N62ZOJPHp8L9oeS6HWMp6lGr9zU7G5p5rDyIcdVePVCyxLiHthMY21dt4PeIAc3PQh";
            }

            public sealed class SearchForDuplicatesApplication
            {
                public const string SEARCH_FOR_DUPLICATES = "FEE7F61E-0F90-4C2D-8ADB-A70E7D2716AC";
                public static readonly string[] Versions = {"1.0.0.0", "1.1.1.1"};
            }

            public sealed class SecureMemo
            {
                public const string SECURE_MEMO_BASE = "16601751-EA78-4B87-AD93-9135B9DF8FC8";
                public static readonly string[] Versions = {"1.0.0.0", "1.1.0.0"};
            }
        }
    }
}