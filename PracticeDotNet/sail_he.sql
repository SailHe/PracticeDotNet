/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : sail_he

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2018-11-03 20:17:47
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户主键',
  `user_name` varchar(255) DEFAULT NULL COMMENT '用户名',
  `user_realname` varchar(255) DEFAULT NULL COMMENT '真实姓名',
  `user_nickname` varchar(255) DEFAULT NULL COMMENT '昵称',
  `user_age` varchar(255) DEFAULT NULL COMMENT '年龄',
  `user_passworld` varchar(255) DEFAULT NULL COMMENT '密码',
  `user_sex` tinyint(4) DEFAULT NULL COMMENT '性别',
  `user_role` tinyint(4) DEFAULT NULL COMMENT '角色(用于权限分配)',
  `user_contact_way` varchar(255) DEFAULT NULL COMMENT '联系方式',
  `create_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '创建时间',
  `edit_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '编辑时间',
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('11', '更新于Wed Oct 10 20:19:46 CST 2018', null, null, '56', null, null, null, null, '2018-10-10 20:19:46', '2018-10-10 20:19:46');

-- ----------------------------
-- Table structure for ucourse
-- ----------------------------
DROP TABLE IF EXISTS `ucourse`;
CREATE TABLE `ucourse` (
  `cid` varchar(255) DEFAULT NULL,
  `cname` varchar(255) DEFAULT NULL,
  `credit` varchar(255) DEFAULT NULL,
  `pcid` varchar(255) DEFAULT NULL,
  `chour` varchar(255) DEFAULT NULL,
  `cattr` varchar(255) DEFAULT NULL,
  `cnum` varchar(255) DEFAULT NULL,
  `did` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ucourse
-- ----------------------------
INSERT INTO `ucourse` VALUES ('1', '数据库              ', '4', '5', '72', '必修  ', '50', 'CS');
INSERT INTO `ucourse` VALUES ('10', '统计与审计          ', '5', '9', '90', '必修  ', '90', 'EM');
INSERT INTO `ucourse` VALUES ('11', '刺绣                ', '1', 'NULL', '18', '选修  ', '50', 'EL');
INSERT INTO `ucourse` VALUES ('12', '家庭保健            ', '1', 'NULL', '18', '选修  ', '30', 'EL');
INSERT INTO `ucourse` VALUES ('2', '数学                ', '3', 'NULL', '54', '必修  ', '90', 'SD');
INSERT INTO `ucourse` VALUES ('3', '信息系统与数据库    ', '3', '1', '54', '必修  ', '50', 'CS');
INSERT INTO `ucourse` VALUES ('4', '操作系统            ', '4', '6', '72', '必修  ', '50', 'CS');
INSERT INTO `ucourse` VALUES ('5', '数据结构            ', '5', '7', '90', '必修  ', '50', 'CS');
INSERT INTO `ucourse` VALUES ('6', '计算机基础          ', '3', 'NULL', '54', '必修  ', '70', 'CS');
INSERT INTO `ucourse` VALUES ('7', 'C语言               ', '2', '6', '36', '必修  ', '70', 'CS');
INSERT INTO `ucourse` VALUES ('8', '计算机组成原理      ', '3', 'NULL', '54', '选修  ', '120', 'CS');
INSERT INTO `ucourse` VALUES ('9', '会计学原理          ', '5', '2', '90', '必修  ', '90', 'EM');

-- ----------------------------
-- Table structure for udept
-- ----------------------------
DROP TABLE IF EXISTS `udept`;
CREATE TABLE `udept` (
  `did` varchar(255) DEFAULT NULL,
  `dname` varchar(255) DEFAULT NULL,
  `daddr` varchar(255) DEFAULT NULL,
  `dtele` varchar(255) DEFAULT NULL,
  `demail` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of udept
-- ----------------------------
INSERT INTO `udept` VALUES ('CS', '计算机科学与技术系', 'SL604', '87678976', 'cs@nit.com');
INSERT INTO `udept` VALUES ('EL', '外校', 'NULL', '62765678', 'NULL');
INSERT INTO `udept` VALUES ('EM', '经济管理系', 'SC302', '87464789', 'em@nit.com');
INSERT INTO `udept` VALUES ('FD', '外语分院', 'SA401', '65656798', 'fd@nit.com');
INSERT INTO `udept` VALUES ('IT', '信息科学与技术系', 'SL704', '88767864', 'it@nit.com');
INSERT INTO `udept` VALUES ('SD', '理学院', 'NB309', '67536387', 'sd@nit.com');

-- ----------------------------
-- Table structure for ugrade
-- ----------------------------
DROP TABLE IF EXISTS `ugrade`;
CREATE TABLE `ugrade` (
  `gid` varchar(255) DEFAULT NULL,
  `gname` varchar(255) DEFAULT NULL,
  `gyear` varchar(255) DEFAULT NULL,
  `did` varchar(255) DEFAULT NULL,
  `tid` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ugrade
-- ----------------------------
INSERT INTO `ugrade` VALUES ('1', '计算机科学与技术1班', '2006', 'CS', '2008');
INSERT INTO `ugrade` VALUES ('2', '计算机科学与技术2班', '2006', 'CS', '2008');
INSERT INTO `ugrade` VALUES ('3', '计算机科学与技术3班', '2006', 'CS', '2008');
INSERT INTO `ugrade` VALUES ('4', '软件工程1班', '2007', 'CS', '2009');
INSERT INTO `ugrade` VALUES ('5', '软件工程2班', '2007', 'CS', 'NULL');
INSERT INTO `ugrade` VALUES ('6', '自动化1班', '2008', 'IT', '3014');
INSERT INTO `ugrade` VALUES ('7', '自动化2班', '2008', 'IT', '3014');
INSERT INTO `ugrade` VALUES ('8', '电子信息1班', '2008', 'IT', '2006');
INSERT INTO `ugrade` VALUES ('9', '电子信息2班', '2008', 'IT', 'NULL');
INSERT INTO `ugrade` VALUES ('10', '电子信息3班', '2008', 'IT', 'NULL');
INSERT INTO `ugrade` VALUES ('11', '财务管理', '2006', 'EM', '3010');
INSERT INTO `ugrade` VALUES ('12', '旅游管理', '2006', 'EM', '3011');
INSERT INTO `ugrade` VALUES ('13', '营销管理', '2006', 'EM', 'NULL');
INSERT INTO `ugrade` VALUES ('14', '信息管理', '2006', 'EM', 'NULL');
INSERT INTO `ugrade` VALUES ('15', '日语', '2005', 'FD', '3012');
INSERT INTO `ugrade` VALUES ('16', '德语1班', '2005', 'FD', '3013');
INSERT INTO `ugrade` VALUES ('17', '德语2班', '2005', 'FD', '3013');
INSERT INTO `ugrade` VALUES ('18', '应用数学1班', '2008', 'SD', 'NULL');
INSERT INTO `ugrade` VALUES ('20', '选修混合', 'NULL', 'NULL', 'NULL');

-- ----------------------------
-- Table structure for ujobtable
-- ----------------------------
DROP TABLE IF EXISTS `ujobtable`;
CREATE TABLE `ujobtable` (
  `jid` varchar(255) DEFAULT NULL,
  `cid` varchar(255) DEFAULT NULL,
  `room` varchar(255) DEFAULT NULL,
  `tid` varchar(255) DEFAULT NULL,
  `week` varchar(255) DEFAULT NULL,
  `timeseg` varchar(255) DEFAULT NULL,
  `gid` varchar(255) DEFAULT NULL,
  `term` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ujobtable
-- ----------------------------
INSERT INTO `ujobtable` VALUES ('1', '1', 'NB222     ', '2001', '4', '123', '1', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('2', '1', 'NB222     ', '2002', '2', '345', '1', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('3', '2', 'NB222     ', '2003', '1', '34', '3', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('4', '3', 'NB223     ', '2004', '5', '678', '1', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('5', '5', 'NB224     ', '2005', '3', '34', '5', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('6', '6', 'NB225     ', '2006', '1', '67', '5', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('7', '7', 'NB226     ', '2007', '4', '89', '3', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('8', '9', 'NB227     ', '2008', '4', '678', '8', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('9', '11', 'NB228     ', '2009', '2', 'AB      ', '9', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('10', '2', 'NB229     ', '3010', '1', '123', '2', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('11', '4', 'NB230     ', '2001', '5', '345', '2', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('12', '5', 'NB231     ', '2002', '3', '123', '3', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('13', '6', 'NB222     ', '2003', '1', '89', '9', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('14', '7', 'NB223     ', '2004', '4', '67', '13', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('15', '9', 'NB224     ', '2005', '4', '345', '15', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('16', '3', 'NB225     ', '2006', '2', '89', '13', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('17', '4', 'NB226     ', '2007', '2', '12', '15', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('18', '10', 'NB227     ', '2008', '5', '123', '3', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('19', '10', 'NB228     ', '2009', '3', 'AB      ', 'Z0', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('20', '12', 'NB229     ', '3010', '1', 'AB      ', 'Z0', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('21', '4', 'NB230     ', '2001', '2', '34', '12', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('22', '3', 'NB231     ', '2002', '4', '345', '9', '2008-2009-1 ');
INSERT INTO `ujobtable` VALUES ('24', '1', 'NB222     ', '2003', '5', '12', '9', '2008-2009-1 ');

-- ----------------------------
-- Table structure for usc
-- ----------------------------
DROP TABLE IF EXISTS `usc`;
CREATE TABLE `usc` (
  `scid` varchar(255) DEFAULT NULL,
  `sid` varchar(255) DEFAULT NULL,
  `cid` varchar(255) DEFAULT NULL,
  `term` varchar(255) DEFAULT NULL,
  `score1` varchar(255) DEFAULT NULL,
  `score2` varchar(255) DEFAULT NULL,
  `score3` varchar(255) DEFAULT NULL,
  `score` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of usc
-- ----------------------------
INSERT INTO `usc` VALUES ('1', '12005001', '1', '2008-2009-1 ', '93', '90', '', '');
INSERT INTO `usc` VALUES ('2', '12005002', '1', '2008-2009-1 ', '72', '89', '', '');
INSERT INTO `usc` VALUES ('3', '12005003', '1', '2008-2009-1 ', '89', '89', '', '');
INSERT INTO `usc` VALUES ('4', '12005004', '1', '2008-2009-1 ', '86', '78', '', '');
INSERT INTO `usc` VALUES ('5', '32005005', '1', '2008-2009-1 ', '80', '65', '', '');
INSERT INTO `usc` VALUES ('6', '32005006', '1', '2008-2009-1 ', '88', '76', '', '');
INSERT INTO `usc` VALUES ('7', '32005007', '1', '2008-2009-1 ', '67', '56', '', '');
INSERT INTO `usc` VALUES ('8', '12005001', '4', '2008-2009-1 ', '67', '50', '', '');
INSERT INTO `usc` VALUES ('9', '12005002', '4', '2008-2009-1 ', '36', '78', '', '');
INSERT INTO `usc` VALUES ('10', '12005004', '4', '2008-2009-1 ', '87', '98', '', '');
INSERT INTO `usc` VALUES ('11', '32005007', '4', '2008-2009-1 ', '54', '79', '', '');
INSERT INTO `usc` VALUES ('12', '12005001', '3', '2008-2009-2 ', '45', '93', '', '');
INSERT INTO `usc` VALUES ('13', '12005002', '3', '2008-2009-2 ', '78', '48', '', '');
INSERT INTO `usc` VALUES ('14', '12005003', '3', '2008-2009-2 ', '72', '89', '', '');
INSERT INTO `usc` VALUES ('15', '12005004', '3', '2008-2009-2 ', '60', '86', '', '');
INSERT INTO `usc` VALUES ('16', '32005005', '8', '2008-2009-2 ', '60', '80', '', '');
INSERT INTO `usc` VALUES ('17', '32005006', '8', '2008-2009-2 ', '90', '88', '', '');
INSERT INTO `usc` VALUES ('18', '32005007', '8', '2008-2009-2 ', '89', '67', '', '');
INSERT INTO `usc` VALUES ('19', '82005008', '8', '2008-2009-2 ', '93', '65', '', '');
INSERT INTO `usc` VALUES ('20', '82005009', '5', '2008-2009-2 ', '72', '78', '', '');
INSERT INTO `usc` VALUES ('21', '92005010', '5', '2008-2009-2 ', '89', '77', '', '');
INSERT INTO `usc` VALUES ('22', '112005011', '5', '2008-2009-2 ', '86', '90', '', '');
INSERT INTO `usc` VALUES ('23', '112005012', '5', '2008-2009-2 ', '80', '45', '', '');
INSERT INTO `usc` VALUES ('24', '112005013', '5', '2008-2009-2 ', '88', '89', '', '');
INSERT INTO `usc` VALUES ('25', '12006001', '10', '2008-2009-2 ', '67', '67', '', '');
INSERT INTO `usc` VALUES ('26', '12006002', '10', '2008-2009-2 ', '67', '36', '', '');
INSERT INTO `usc` VALUES ('27', '12006003', '10', '2008-2009-2 ', '36', '87', '', '');
INSERT INTO `usc` VALUES ('28', '12006004', '10', '2008-2009-2 ', '87', '54', '', '');
INSERT INTO `usc` VALUES ('29', '32006005', '10', '2008-2009-2 ', '54', '45', '', '');
INSERT INTO `usc` VALUES ('30', '32006006', '10', '2008-2009-2 ', '45', '78', '', '');
INSERT INTO `usc` VALUES ('31', '32006007', '10', '2008-2009-2 ', '78', '72', '', '');
INSERT INTO `usc` VALUES ('32', '82006008', '10', '2007-2008-2 ', '72', '60', '', '');
INSERT INTO `usc` VALUES ('33', '82006009', '12', '2007-2008-2 ', '60', '60', '', '');
INSERT INTO `usc` VALUES ('34', '92006010', '12', '2007-2008-2 ', '60', '90', '', '');
INSERT INTO `usc` VALUES ('35', '112006011', '12', '2007-2008-2 ', '90', '89', '', '');
INSERT INTO `usc` VALUES ('36', '112006012', '12', '2007-2008-2 ', '89', '89', '', '');
INSERT INTO `usc` VALUES ('37', '112006013', '12', '2007-2008-2 ', '93', '78', '', '');
INSERT INTO `usc` VALUES ('38', '12007001', '11', '2007-2008-2 ', '72', '65', '', '');
INSERT INTO `usc` VALUES ('39', '12007002', '11', '2007-2008-2 ', '89', '76', '', '');
INSERT INTO `usc` VALUES ('40', '12007003', '11', '2007-2008-2 ', '86', '56', '', '');
INSERT INTO `usc` VALUES ('41', '12007004', '11', '2007-2008-2 ', '80', '50', '', '');
INSERT INTO `usc` VALUES ('42', '32007005', '11', '2007-2008-2 ', '88', '78', '', '');
INSERT INTO `usc` VALUES ('43', '32007006', '11', '2007-2008-2 ', '67', '98', '', '');
INSERT INTO `usc` VALUES ('44', '32007007', '11', '2007-2008-2 ', '67', '79', '', '');
INSERT INTO `usc` VALUES ('45', '82007008', '9', '2007-2008-2 ', '36', '93', '', '');
INSERT INTO `usc` VALUES ('46', '82007009', '9', '2007-2008-2 ', '87', '72', '', '');
INSERT INTO `usc` VALUES ('47', '92007010', '9', '2008-2009-1 ', '54', '89', '', '');
INSERT INTO `usc` VALUES ('48', '112007011', '9', '2008-2009-1 ', '45', '86', '', '');
INSERT INTO `usc` VALUES ('49', '112007012', '9', '2008-2009-1 ', '78', '80', '', '');
INSERT INTO `usc` VALUES ('50', '112007013', '9', '2008-2009-1 ', '72', '88', '', '');
INSERT INTO `usc` VALUES ('51', '12005001', '9', '2008-2009-1 ', '60', '67', '', '');
INSERT INTO `usc` VALUES ('52', '12005002', '9', '2008-2009-1 ', '60', '54', '', '');
INSERT INTO `usc` VALUES ('53', '12005003', '9', '2008-2009-1 ', '90', '78', '', '');
INSERT INTO `usc` VALUES ('54', '12005004', '9', '2008-2009-1 ', '89', '77', '', '');
INSERT INTO `usc` VALUES ('55', '32005005', '2', '2008-2009-1 ', '93', '90', '', '');
INSERT INTO `usc` VALUES ('56', '32005006', '2', '2008-2009-1 ', '72', '45', '', '');
INSERT INTO `usc` VALUES ('57', '32005007', '2', '2008-2009-1 ', '89', '89', '', '');
INSERT INTO `usc` VALUES ('58', '82005008', '2', '2008-2009-1 ', '86', '67', '', '');
INSERT INTO `usc` VALUES ('59', '82005009', '2', '2008-2009-1 ', '80', '36', '', '');
INSERT INTO `usc` VALUES ('60', '92005010', '2', '2008-2009-1 ', '88', '87', '', '');
INSERT INTO `usc` VALUES ('61', '112005011', '2', '2008-2009-1 ', '67', '54', '', '');
INSERT INTO `usc` VALUES ('62', '112005012', '2', '2008-2009-1 ', '67', '45', '', '');
INSERT INTO `usc` VALUES ('63', '112005013', '2', '2008-2009-1 ', '36', '78', '', '');
INSERT INTO `usc` VALUES ('64', '12006001', '2', '2008-2009-1 ', '87', '72', '', '');
INSERT INTO `usc` VALUES ('65', '12006002', '6', '2008-2009-1 ', '54', '60', '', '');
INSERT INTO `usc` VALUES ('66', '12006003', '6', '2008-2009-1 ', '45', '60', '', '');
INSERT INTO `usc` VALUES ('67', '12006004', '6', '2008-2009-1 ', '78', '90', '', '');
INSERT INTO `usc` VALUES ('68', '32006005', '6', '2008-2009-1 ', '72', '89', '', '');
INSERT INTO `usc` VALUES ('69', '32006006', '6', '2008-2009-1 ', '60', '89', '', '');
INSERT INTO `usc` VALUES ('70', '32006007', '6', '2008-2009-1 ', '60', '78', '', '');
INSERT INTO `usc` VALUES ('71', '82006008', '6', '2008-2009-1 ', '90', '65', '', '');
INSERT INTO `usc` VALUES ('72', '82006009', '6', '2008-2009-1 ', '89', '76', '', '');
INSERT INTO `usc` VALUES ('73', '92006010', '6', '2007-2008-1 ', '93', '56', '', '');
INSERT INTO `usc` VALUES ('74', '112006011', '6', '2007-2008-1 ', '72', '50', '', '');
INSERT INTO `usc` VALUES ('75', '112006012', '6', '2007-2008-1 ', '89', '78', '', '');
INSERT INTO `usc` VALUES ('76', '112006013', '6', '2007-2008-1 ', '86', '98', '', '');
INSERT INTO `usc` VALUES ('77', '12007001', '7', '2007-2008-1 ', '80', '79', '', '');
INSERT INTO `usc` VALUES ('78', '12007002', '7', '2007-2008-1 ', '88', '93', '', '');
INSERT INTO `usc` VALUES ('79', '12007003', '7', '2007-2008-1 ', '67', '72', '', '');
INSERT INTO `usc` VALUES ('80', '12007004', '7', '2007-2008-1 ', '67', '89', '', '');
INSERT INTO `usc` VALUES ('81', '32007005', '7', '2007-2008-1 ', '36', '86', '', '');
INSERT INTO `usc` VALUES ('82', '32007006', '7', '2007-2008-1 ', '87', '80', '', '');
INSERT INTO `usc` VALUES ('83', '32007007', '7', '2007-2008-1 ', '54', '88', '', '');
INSERT INTO `usc` VALUES ('84', '82007008', '7', '2007-2008-1 ', '45', '67', '', '');
INSERT INTO `usc` VALUES ('85', '82007009', '7', '2007-2008-1 ', '78', '65', '', '');
INSERT INTO `usc` VALUES ('86', '92007010', '7', '2007-2008-1 ', '72', '78', '', '');
INSERT INTO `usc` VALUES ('87', '112007011', '7', '2007-2008-1 ', '60', '77', '', '');
INSERT INTO `usc` VALUES ('88', '112007012', '7', '2007-2008-1 ', '60', '90', '', '');
INSERT INTO `usc` VALUES ('89', '112007013', '7', '2007-2008-1 ', '90', '45', '', '');

-- ----------------------------
-- Table structure for ustudent
-- ----------------------------
DROP TABLE IF EXISTS `ustudent`;
CREATE TABLE `ustudent` (
  `sid` int(255) NOT NULL AUTO_INCREMENT,
  `sname` varchar(255) DEFAULT NULL,
  `ssexy` varchar(3) DEFAULT NULL,
  `sbdate` varchar(255) DEFAULT NULL,
  `gid` varchar(255) DEFAULT NULL,
  `stele` varchar(13) DEFAULT NULL,
  PRIMARY KEY (`sid`)
) ENGINE=InnoDB AUTO_INCREMENT=112007019 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ustudent
-- ----------------------------
INSERT INTO `ustudent` VALUES ('12005001', '李山', '男', '1988/10/11', '1', '660780');
INSERT INTO `ustudent` VALUES ('12005002', '张飞', '男', '1987/10/11', '1', '660781');
INSERT INTO `ustudent` VALUES ('12005003', '李玉和', '女', '1989/10/11', '1', '660782');
INSERT INTO `ustudent` VALUES ('12005004', '王一飞', '女', '1990/10/11', '1', '660783');
INSERT INTO `ustudent` VALUES ('12006001', '韦宝', '男', '1988/10/11', '1', '660780');
INSERT INTO `ustudent` VALUES ('12006002', '李飞', '男', '1987/10/11', '1', '660781');
INSERT INTO `ustudent` VALUES ('12006003', '冯玉', '女', '1989/10/11', '1', '660782');
INSERT INTO `ustudent` VALUES ('12006004', '马观', '女', '1990/10/11', '1', '660783');
INSERT INTO `ustudent` VALUES ('12007001', '李知', '男', '1988/10/11', '1', '660780');
INSERT INTO `ustudent` VALUES ('12007002', '吴飞', '男', '1987/10/11', '1', '660781');
INSERT INTO `ustudent` VALUES ('12007003', '李凡', '女', '1989/10/11', '1', '660782');
INSERT INTO `ustudent` VALUES ('12007004', '王一飞', '女', '1990/10/11', '1', '660783');
INSERT INTO `ustudent` VALUES ('32005005', '徐红', '女', '1988/1/11', '3', '660784');
INSERT INTO `ustudent` VALUES ('32005006', '刘和', '男', '1988/12/11', '3', '660785');
INSERT INTO `ustudent` VALUES ('32005007', '刘山', '男', '1989/10/1', '3', '660786');
INSERT INTO `ustudent` VALUES ('32006005', '徐一红', '女', '1988/1/11', '3', '660784');
INSERT INTO `ustudent` VALUES ('32006006', '刘一和', '男', '1988/12/11', '3', '660785');
INSERT INTO `ustudent` VALUES ('32006007', '马西', '男', '1989/10/1', '3', '660786');
INSERT INTO `ustudent` VALUES ('32007005', '王红', '女', '1988/1/11', '3', '660784');
INSERT INTO `ustudent` VALUES ('32007006', '王一红', '男', '1988/12/11', '3', '660785');
INSERT INTO `ustudent` VALUES ('32007007', '丁西', '男', '1989/10/1', '3', '660786');
INSERT INTO `ustudent` VALUES ('82005008', '刘去山', '女', '1990/1/11', '8', '660787');
INSERT INTO `ustudent` VALUES ('82005009', '白云飞', '女', '1992/10/11', '8', '660788');
INSERT INTO `ustudent` VALUES ('82006008', '刘问计', '女', '1990/1/11', '8', '660787');
INSERT INTO `ustudent` VALUES ('82006009', '白问礼', '女', '1992/10/11', '8', '660788');
INSERT INTO `ustudent` VALUES ('82007008', '刘红丽', '女', '1990/1/11', '8', '660787');
INSERT INTO `ustudent` VALUES ('82007009', '沈学云', '女', '1992/10/11', '8', '660788');
INSERT INTO `ustudent` VALUES ('92005010', '白云 ', '女', '1988/11/11', '8', '660789');
INSERT INTO `ustudent` VALUES ('92006010', '白云', '女', '1988/12/11', '9', '660788');
INSERT INTO `ustudent` VALUES ('92007010', '李风', '女', '1988/11/11', '9', '660789');
INSERT INTO `ustudent` VALUES ('112005011', '李红', '女', '1987/12/11', '11', '660790');
INSERT INTO `ustudent` VALUES ('112005012', '周磊', '男', '1986/6/11', '11', '660791');
INSERT INTO `ustudent` VALUES ('112005013', '冯圭', '女', '1987/8/11', '11', '660792');
INSERT INTO `ustudent` VALUES ('112006011', '李玉红', '女', '1987/12/11', '11', '660790');
INSERT INTO `ustudent` VALUES ('112006012', '冯磊', '男', '1996/6/11', '11', '660791');
INSERT INTO `ustudent` VALUES ('112006013', '冯由', '女', '1997/8/11', '11', '660792');
INSERT INTO `ustudent` VALUES ('112007011', '刘好', '女', '1987/12/11', '11', '660790');
INSERT INTO `ustudent` VALUES ('112007012', '周成', '男', '1986/6/11', '11', '660791');
INSERT INTO `ustudent` VALUES ('112007013', '文成', '女', '1987/8/11', '11', '660792');
INSERT INTO `ustudent` VALUES ('112007018', '新欣欣', '男', '1996/9/9 0:00:00', '20', '15256598365');

-- ----------------------------
-- Table structure for uteacher
-- ----------------------------
DROP TABLE IF EXISTS `uteacher`;
CREATE TABLE `uteacher` (
  `tid` varchar(255) DEFAULT NULL,
  `tname` varchar(255) DEFAULT NULL,
  `tsexy` varchar(255) DEFAULT NULL,
  `tbdate` varchar(255) DEFAULT NULL,
  `tfield` varchar(255) DEFAULT NULL,
  `tprof` varchar(255) DEFAULT NULL,
  `tele` varchar(255) DEFAULT NULL,
  `qq` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `msn` varchar(255) DEFAULT NULL,
  `did` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of uteacher
-- ----------------------------
INSERT INTO `uteacher` VALUES ('2001', '李飞    ', '男', '1986/5/5', '数据库                                            ', '讲师      ', '660001', 'NULL', 'NULL', 'NULL', 'CS');
INSERT INTO `uteacher` VALUES ('2002', '郭山    ', '男', '1980/6/9', '数据库                                            ', '副教授    ', '660002', 'NULL', 'NULL', 'NULL', 'CS');
INSERT INTO `uteacher` VALUES ('2003', '马骊    ', '女', '1967/12/6', '网络技术与数据库                                  ', '教授      ', '660003', 'NULL', 'NULL', 'NULL', 'CS');
INSERT INTO `uteacher` VALUES ('2004', '徐守    ', '女', '1967/7/14', '面向对象编程                                      ', '助教      ', '660004', 'NULL', 'NULL', 'NULL', 'CS');
INSERT INTO `uteacher` VALUES ('2005', '金贵    ', '女', '1983/3/8', '财务管理                                          ', '助教      ', '660005', 'NULL', 'NULL', 'NULL', 'CS');
INSERT INTO `uteacher` VALUES ('2006', '成山云  ', '男', '1984/11/2', '金融学                                            ', '教授      ', '660006', 'NULL', 'NULL', 'NULL', 'IT');
INSERT INTO `uteacher` VALUES ('2007', '张田下  ', '男', '1977/1/7', '新能源技术                                        ', '研究员    ', '660007', 'NULL', 'NULL', 'NULL', 'IT');
INSERT INTO `uteacher` VALUES ('2008', '王一钱  ', '女', '1965/3/3', '信息管理与数据库                                  ', '讲师      ', '660008', 'NULL', 'NULL', 'NULL', 'IT');
INSERT INTO `uteacher` VALUES ('2009', '李远    ', '女', '1963/9/23', '电工学                                            ', '实验师    ', '660009', 'NULL', 'NULL', 'NULL', 'IT');
INSERT INTO `uteacher` VALUES ('3010', '吴天贵  ', '女', '1984/4/26', '中医学                                            ', '主治医师  ', '660010', 'NULL', 'NULL', 'NULL', 'IT');
INSERT INTO `uteacher` VALUES ('3011', '刘了了  ', '女', '1972/6/12', '近代史                                            ', '研究员    ', '660220', 'NULL', 'NULL', 'NULL', 'EM');
INSERT INTO `uteacher` VALUES ('3012', '洪玉飞  ', '男', '1967/9/29', '哲学                                              ', '教授      ', '660222', 'NULL', 'NULL', 'NULL', 'EM');
INSERT INTO `uteacher` VALUES ('3013', '划计成  ', '女', '1962/9/1', '应用数学                                          ', '教授      ', '660223', 'NULL', 'NULL', 'NULL', 'EM');
INSERT INTO `uteacher` VALUES ('3014', '李丽青  ', '男', '1968/5/9', '应用物理                                          ', '讲师      ', '660233', 'NULL', 'NULL', 'NULL', 'EM');
INSERT INTO `uteacher` VALUES ('3015', '李员    ', '男', '1971/9/15', '统计学                                            ', '研究员    ', '660234', 'NULL', 'NULL', 'NULL', 'FD');
INSERT INTO `uteacher` VALUES ('3016', '国威    ', '女', '1965/4/30', '政治学                                            ', '研究员    ', '660123', 'NULL', 'NULL', 'NULL', 'FD');
INSERT INTO `uteacher` VALUES ('3017', '国华    ', '女', '1989/5/29', '证券投资                                          ', '研究员    ', '660987', 'NULL', 'NULL', 'NULL', 'FD');
INSERT INTO `uteacher` VALUES ('5022', '后羿    ', '女', '1983/6/16', '护理与营养                                        ', '主任护理师', '660909', 'NULL', 'NULL', 'NULL', 'SD');
INSERT INTO `uteacher` VALUES ('5023', '王飞红  ', '男', '1975/11/20', '多媒体技术                                        ', '讲师      ', '660938', 'NULL', 'NULL', 'NULL', 'SD');
INSERT INTO `uteacher` VALUES ('5024', '李丽青  ', '男', '1969/3/30', '理论力学                                          ', '副教授    ', '660323', 'NULL', 'NULL', 'NULL', 'SD');
INSERT INTO `uteacher` VALUES ('5025', '王红    ', '女', '1970/6/15', '建筑学                                            ', '副教授    ', '660099', 'NULL', 'NULL', 'NULL', 'SD');
INSERT INTO `uteacher` VALUES ('5026', '李飞    ', '女', '1963/12/22', '流体力学                                          ', '讲师      ', '660987', 'NULL', 'NULL', 'NULL', 'SD');
