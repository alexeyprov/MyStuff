<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:cl="http://nikolayzl2k/pivotal/ChangesLog.xsd">
<xsl:template match="/cl:tasks">
<html>
	<head>
		<title>Changes Log for <xsl:value-of select="@project"/></title>
		<script language="javascript">
			function ShowHideObject(obj)
			{
				obj.style.display = ("none" == obj.style.display) ? "block" : "none";
			}
		</script>
	</head>
	<body>
		<p>
		This file represents log of changes made to <xsl:value-of select="@project"/> system.
		Changes are grouped by tasks. In order to show (hide) task contents just click on its caption. 
		You can also use the table of contents below.
		</p>
		<h2 align="center">Completed tasks</h2>
		<ul>
<xsl:for-each select="//cl:tasks/cl:task">
	<li>
	<xsl:element name="a">
		<xsl:attribute name="href">#anchor<xsl:number format="1"/></xsl:attribute>
		<xsl:value-of select="@name"/>
	</xsl:element>
	</li>
</xsl:for-each>			
		</ul>
		
<xsl:for-each select="//cl:tasks/cl:task">
	<xsl:element name="a">
		<xsl:attribute name="name">anchor<xsl:number format="1"/></xsl:attribute>
	</xsl:element>
	<xsl:element name="h2">
		<xsl:attribute name="onclick">ShowHideObject(spread<xsl:number format="1"/>)</xsl:attribute>
		<xsl:attribute name="style">CURSOR: hand</xsl:attribute>
		<xsl:value-of select="@name"/>
	</xsl:element>
	
	<xsl:element name="div"> <!-- begin spread #i-->
		<xsl:attribute name="id">spread<xsl:number format="1"/></xsl:attribute>
		<p>
		Task was performed at server <i><xsl:value-of select="@server"/></i>
		</p>
		<xsl:element name="h3">
			<xsl:attribute name="onclick">ShowHideObject(steps<xsl:number format="1"/>)</xsl:attribute>
			<xsl:attribute name="style">CURSOR: hand; COLOR: blue</xsl:attribute>
			Steps
		</xsl:element>
		
		<xsl:element name="div">
			<xsl:attribute name="id">steps<xsl:number format="1"/></xsl:attribute>
			Changes made to model
			<table width="90%" border="1">
				<tr>
					<th width="5%">ED/BM</th>
					<th width="10%">Date</th>
					<th>Object Type</th>
					<th>Object Name</th>
					<th>Action</th>
				</tr>
			<xsl:for-each select="cl:step">
				<tr><xsl:apply-templates select="."/></tr>
			</xsl:for-each>
			</table>
		</xsl:element>
		
		<xsl:element name="h3">
			<xsl:attribute name="onclick">ShowHideObject(sec<xsl:number format="1"/>)</xsl:attribute>
			<xsl:attribute name="style">CURSOR: hand; COLOR: blue</xsl:attribute>
			Security
		</xsl:element>
		
		<xsl:element name="div">
			<xsl:attribute name="id">sec<xsl:number format="1"/></xsl:attribute>
			Changes made to security
			<table width="90%" border="1">
				<tr>
					<th>Object</th>
					<th>Security Group</th>
					<th>Access Granted/Revoked</th>
				<xsl:for-each select="cl:security/cl:privelege">
					<tr><xsl:apply-templates select="."/></tr>
				</xsl:for-each>
				</tr>
			</table>
		</xsl:element>
		
	</xsl:element> <!-- end spread #i-->
	
</xsl:for-each>
	</body>
</html>
</xsl:template>
<xsl:template match="cl:step">
	<td><xsl:value-of select="@model"/></td>
	<td><xsl:value-of select="@date"/></td>
	<td><xsl:value-of select="cl:object/@type"/></td>
	<td><xsl:value-of select="cl:object"/></td>
	<td><xsl:value-of select="cl:object/@action"/></td>
</xsl:template>
<xsl:template match="cl:privelege">
	<td><xsl:value-of select="cl:subject"/></td>
	<td><xsl:value-of select="cl:grantee"/></td>
	<td><xsl:value-of select="@action"/></td>
</xsl:template>
</xsl:stylesheet>

  