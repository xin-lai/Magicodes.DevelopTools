# Magicodes.DevelopTools   开发者工具

### Magicodes.CmdTools  命令行辅助工具

#### copy	复制文件
	-s  源路径（必填）
	-t	目标路径（必填）
	-c	配置文件路径
    -i  忽略的文件名&扩展名&目录名（多个请用分号分割）
	-d	调试模式，默认false


#### order	文本排序
	-s  源路径（必填）
	-t	目标路径

#### replace	替换文件或文本
	-p  目录或文件路径（必填）
	-s  源字符串（多个请以空格分隔）（必填）
	-t	目标字符串（多个请以空格分隔）（必填）
    -i  是否替换文本(bool)


#### Demo:

	call "tools/Magicodes.CmdTools.exe" order -s "changeList.txt"
	将文件"changeList.txt"内的文本进行排序，并去除重复项以及首尾空格

	call "tools/Magicodes.CmdTools.exe" copy -s "../../Magicodes.Admin" -t "../" -c "changeList.txt"
	根据配置"changeList.txt"从"../../Magicodes.Admin"目录复制文件到"../"目录

    call "$(SolutionDir)tools\tools\Magicodes.CmdTools.exe" copy -i ".cs" -s "$(SolutionDir)plus\Magicodes.Test\Magicodes.Test.Web.Mvc\Views" -t "$(SolutionDir)src\Magicodes.Admin.Web.Mvc\wwwroot\PlugIns\Magicodes.Test.Web.Mvc\Views"
    此代码需设置在VS工程文件的生成事件之中，用于生成成功时复制相关文件到插件目录。这里复制视图到插件目录并且忽略类文件

    call "Magicodes.CmdTools.exe" replace -s "Magicodes.Test Test" -t "$(companyName).$(projectName) $(projectName)" -p "./tps" -i true
    将字符串Magicodes.Test和Test（包括文件名中的此字符串）替换为$(companyName).$(projectName)和$(projectName)，替换目录为当前目录下的tps目录下的所有文件或目录以及文件内容

    call "Magicodes.CmdTools.exe" replace -s "<#=modelTypeNameSpace#> <#=tPrimaryKey#> <#=modelTypeVarName#> <#=repositoryName#> businessPropertys" -t "<#=host.ModelLastNameSpace#> <#=host.PrimaryKeyShortTypeName#> <#=host.ModelVarName#> <#=host.RepositoryVarName#> host.BusinessProperties" -p "./tps/ModelProject" -i true
    将字符串<#=modelTypeNameSpace#>、<#=tPrimaryKey#>、<#=modelTypeVarName#>、<#=repositoryName#> 、businessPropertys（包括文件名中的此字符串）替换为<#=host.ModelLastNameSpace#>、<#=host.PrimaryKeyShortTypeName#>、<#=host.ModelVarName#>、<#=host.RepositoryVarName#>、host.BusinessProperties，替换目录为当前目录下的tps/ModelProject目录下的所有文件或目录以及文件内容