## Magicodes.DevelopTools   开发者工具

Magicodes.CmdTools  命令行辅助工具
copy	复制文件
	-s  源路径（必填）
	-t	目标路径（必填）
	-c	配置文件路径
	-d	调试模式，默认false

order	文本排序
	-s  源路径（必填）
	-t	目标路径

Demo:
	call "tools/Magicodes.CmdTools.exe" order -s "changeList.txt"
	将文件"changeList.txt"内的文本进行排序，并去除重复项以及首尾空格

	call "tools/Magicodes.CmdTools.exe" copy -s "../../Magicodes.Admin" -t "../" -c "changeList.txt"
	根据配置"changeList.txt"从"../../Magicodes.Admin"目录复制文件到"../"目录
